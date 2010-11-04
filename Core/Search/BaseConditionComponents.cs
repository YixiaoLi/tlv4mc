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
        private ComboBox _baseTargetResourceForm;
        private ComboBox _baseTargetRuleForm;
        private ComboBox _baseTargetEventForm;
        private ComboBox _baseTargetEventDetailForm;
        private ConditionRegister _conditionRegister;

        public BaseConditionComponents(TraceLogVisualizerData data)
        {
            _data = data;
            _conditioRegister = new ConditionBeans
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


                mainRuleForm.Enabled = false;
                mainEventForm.Enabled = false;
                mainEventDetailForm.Enabled = false;
            }

            arrangeDropDownSize(mainResourceForm);
        }

        //リソースが選択されたときにルール指定コンボボックスのアイテムをセットする
        private void makeMainRuleForm()
        {
            mainRuleForm.Items.Clear();
            //選ばれているリソースの種類を調べる
            _conditionRegister.mainResourceType = _data.ResourceData.Resources[(string)mainResourceForm.SelectedItem].Type;
            GeneralNamedCollection<VisualizeRule> visRules = _data.VisualizeData.VisualizeRules;

            foreach (VisualizeRule rule in visRules)
            {
                if (rule.Target != null && rule.Target.Equals(_conditionRegister.mainResourceType))
                {
                    mainRuleForm.Items.Add(rule.DisplayName);
                }
            }
            arrangeDropDownSize(mainRuleForm);
        }


        //イベント指定コンボボックスのアイテムをセット
        private void makeMainEventForm()
        {
            mainEventForm.Items.Clear();
            //選択されているルール名(例："状態遷移")の正式名称(例："taskStateChange")を調べる
            foreach (VisualizeRule visRule in _data.VisualizeData.VisualizeRules)
            {
                if (visRule.Target == null) // ルールのターゲットは CurrentContext
                {
                    _conditionRegister.mainRuleName = visRule.Name;
                }
                else if (visRule.Target.Equals(_conditionRegister.mainResourceType) && visRule.DisplayName.Equals(mainRuleForm.SelectedItem))
                {
                    _conditionRegister.mainRuleName = visRule.Name;
                    break;
                }
            }

            GeneralNamedCollection<Event> eventShapes = _data.VisualizeData.VisualizeRules[_conditionRegister.mainRuleName].Shapes;
            foreach (Event e in eventShapes)
            {
                mainEventForm.Items.Add(e.DisplayName);
            }

            arrangeDropDownSize(mainEventForm);
        }


        //イベント詳細指定コンボボックスのアイテムをセット
        private void makeMainEventDetailForm()
        {
            mainEventDetailForm.Items.Clear();
            //選択されているイベント名(例："状態")の正式名称(例："stateChangeEvent")を調べる
            foreach (Event ev in _data.VisualizeData.VisualizeRules[_conditionRegister.mainRuleName].Shapes)
            {
                if (ev.DisplayName.Equals(mainEventForm.SelectedItem))
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
                    mainEventDetailForm.Enabled = false;
                }
                else
                {
                    foreach (Figure fg2 in fg.Figures)
                    {                                                   // 処理の意図を以下に例示
                        String[] conditions = fg2.Condition.Split('='); // "($FROM_VAL)==RUNNING"  ⇒ "($FROM_VAL)", "","RUNNING"
                        mainEventDetailForm.Items.Add(conditions[2]); // "RUNNING"をイベント詳細のコンボボックスへセット
                    }
                }

            }
            arrangeDropDownSize(mainEventDetailForm);
        }

    }
}
