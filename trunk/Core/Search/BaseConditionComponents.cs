using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    class BaseConditionComponents
    {
        private TraceLogVisualizerData _data;
        private int _componentID; //この検索条件設定コンポーネントを一意に識別するためのID
        private int _formWidth;   //親フォームの横幅。これをもとにComboBox の配置を決定する
        private ComboBox _baseTargetResourceForm;
        private ComboBox _baseTargetRuleForm;
        private ComboBox _baseTargetEventForm;
        private ComboBox _baseTargetEventDetailForm;
        private ConditionRegister _conditionRegister;

        public BaseConditionComponents(TraceLogVisualizerData data, int componentID, int formWidth)
        {
            _data = data;
            _componentID = componentID;
            _formWidth = formWidth;
            _baseTargetResourceForm = new ComboBox();
            _baseTargetRuleForm = new ComboBox();
            _baseTargetEventForm = new ComboBox();
            _baseTargetEventDetailForm = new ComboBox();
            _conditionRegister = new ConditionRegister();
        }

        //リソース指定コンボボックスのアイテムをセット
        private void makeMainResourceForm()
        {
            GeneralNamedCollection<Resource> resData = this._data.ResourceData.Resources;

            foreach (Resource res in resData)
            {
                if (!res.Name.Equals("CurrentContext"))
                    _baseTargetResourceForm.Items.Add(res.Name);
            }

            if (_baseTargetRuleForm.Enabled == true)
            {
                _baseTargetRuleForm.Items.Clear();
                _baseTargetEventForm.Items.Clear();
                _baseTargetEventDetailForm.Items.Clear();

                _conditionRegister.mainResourceName = null;
                _conditionRegister.mainRuleName = null;
                _conditionRegister.mainEventName = null;
                _conditionRegister.mainEventDetail = null;


                _baseTargetRuleForm.Enabled = false;
                _baseTargetEventForm.Enabled = false;
                _baseTargetEventDetailForm.Enabled = false;
            }

            arrangeDropDownSize(_baseTargetResourceForm);
        }

        //リソースが選択されたときにルール指定コンボボックスのアイテムをセットする
        private void makeMainRuleForm()
        {
            _baseTargetRuleForm.Items.Clear();
            //選ばれているリソースの種類を調べる
            _conditionRegister.mainResourceType = _data.ResourceData.Resources[(string)_baseTargetResourceForm.SelectedItem].Type;
            GeneralNamedCollection<VisualizeRule> visRules = _data.VisualizeData.VisualizeRules;

            foreach (VisualizeRule rule in visRules)
            {
                if (rule.Target != null && rule.Target.Equals(_conditionRegister.mainResourceType))
                {
                    _baseTargetRuleForm.Items.Add(rule.DisplayName);
                }
            }
            arrangeDropDownSize(_baseTargetRuleForm);
        }


        //イベント指定コンボボックスのアイテムをセット
        private void makeMainEventForm()
        {
            _baseTargetEventForm.Items.Clear();
            //選択されているルール名(例："状態遷移")の正式名称(例："taskStateChange")を調べる
            foreach (VisualizeRule visRule in _data.VisualizeData.VisualizeRules)
            {
                if (visRule.Target == null) // ルールのターゲットは CurrentContext
                {
                    _conditionRegister.mainRuleName = visRule.Name;
                }
                else if (visRule.Target.Equals(_conditionRegister.mainResourceType) && visRule.DisplayName.Equals(_baseTargetRuleForm.SelectedItem))
                {
                    _conditionRegister.mainRuleName = visRule.Name;
                    break;
                }
            }

            GeneralNamedCollection<Event> eventShapes = _data.VisualizeData.VisualizeRules[_conditionRegister.mainRuleName].Shapes;
            foreach (Event e in eventShapes)
            {
                _baseTargetEventForm.Items.Add(e.DisplayName);
            }

            arrangeDropDownSize(_baseTargetEventForm);
        }


        //イベント詳細指定コンボボックスのアイテムをセット
        private void makeMainEventDetailForm()
        {
            _baseTargetEventDetailForm.Items.Clear();
            //選択されているイベント名(例："状態")の正式名称(例："stateChangeEvent")を調べる
            foreach (Event ev in _data.VisualizeData.VisualizeRules[_conditionRegister.mainRuleName].Shapes)
            {
                if (ev.DisplayName.Equals(_baseTargetEventForm.SelectedItem))
                {
                    _conditionRegister.mainEventName = ev.Name;
                    break;
                }
            }

            //指定されたイベントが持つ RUNNABLE, RUNNING といった状態を切り出す
            Event e = _data.VisualizeData.VisualizeRules[_conditionRegister.mainRuleName].Shapes[_conditionRegister.mainEventName];
            foreach (Figure fg in e.Figures) // いつもe.Figuresの要素は一つしかないが、foreach で回しておく（どんなときに複数の要素を持つかは要調査）
            {
                if (fg.Figures == null) //選択されたイベントにイベント詳細が存在しない場合
                {
                    _baseTargetEventDetailForm.Enabled = false;
                }
                else
                {
                    foreach (Figure fg2 in fg.Figures)
                    {                                                   // 処理の意図を以下に例示
                        String[] conditions = fg2.Condition.Split('='); // "($FROM_VAL)==RUNNING"  ⇒ "($FROM_VAL)", "","RUNNING"
                        _baseTargetEventDetailForm.Items.Add(conditions[2]); // "RUNNING"をイベント詳細のコンボボックスへセット
                    }
                }

            }
            arrangeDropDownSize(_baseTargetEventDetailForm);
        }

        private void arrangeComboBoxSize()
        {
            
        }

        private void arrangeComboBoxLocation()
        {
            
        }

        //コンボボックスのドロップダウンボックスのサイズを自動調整
        private void arrangeDropDownSize(ComboBox targetBox)
        {
            int maxTextLength = 0;
            int font_W = (int)Math.Ceiling(
                             targetBox.Font.SizeInPoints * 2.0F / 3.0F);  // フォント幅を取得

            foreach (string A in targetBox.Items)
            {
                // 各行の文字バイト長から「横幅」を算出し、その最大値を求める
                int len = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(A);
                maxTextLength = Math.Max(maxTextLength, len * font_W);
            }
            targetBox.DropDownWidth = maxTextLength + 10;
        }

    }
}
