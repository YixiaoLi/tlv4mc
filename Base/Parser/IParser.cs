using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
    /// <summary>
    /// ParserとNullObjectを結ぶために用いています。
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// パース後の処理。Begin()と対で使います。
        /// </summary>
        /// <returns></returns>
        IParser End();


        #region 各種基本パーサ
        /*
         * ここにある各種基本パーサ(パースメソッド)へ、
         * サブクラスは同名のメソッドにて委譲します。
         * そうすることで、実質、サブクラスの型にキャストが可能となり、
         * 高い保守性を実現しています。
         * 
         * 実際は、この部分はこのインタフェースに記述しなくても動作します。
         * ＜記述した意図＞
         * 下位インタフェースにて戻り値の型を変更して最低限再定義してほ
         * しいものを示すために設けました。
         * 
         */

        #region パーサコンビネータ
        /// <summary>
        /// 引数で与えられたパーサ(メソッド)を複数回適用する。
        /// 正規表現の"*"に相当する。
        /// </summary>
        /// <typeparam name="TParser">サブクラス(パーサクラス)の型</typeparam>
        /// <param name="f">パーサ(メソッド)</param>
        /// <returns>this</returns>
        IParser Many<TParser>(Func<TParser> f);   // 下位インタフェースでは、非ジェネリクスで定義して可読性を向上させて下さい
        /// <summary>
        /// 引数で与えられたパーサ(メソッド)を1回以上適用する。
        /// 正規表現の"+"に相当する。
        /// </summary>
        /// <typeparam name="TParser">サブクラス(パーサクラス)の型</typeparam>
        /// <param name="f">パーサ(メソッド)</param>
        /// <returns>成功：this、失敗：NullObject</returns>
        IParser Many1<TParser>(Func<TParser> f);   // 下位インタフェースでは、非ジェネリクスで定義して可読性を向上させて下さい
        /// <summary>
        /// パーサ間のORをとる。
        /// ORの前までにパースが失敗した場合、ORの後のパーサで再度パースを試みる。
        /// ORの前までのパーサでパースできた場合、ORの後のパーサは無視する。
        /// </summary>
        /// <returns>これ以前のパースに成功：NullObject</returns>
        IParser OR();
        #endregion


        #region 文字パーサ

        /// <summary>
        /// 指定の一文字をパースする
        /// </summary>
        /// <param name="c">パースしたい文字</param>
        /// <returns>成功：this, 失敗：NullObject</returns>
        IParser Char(char c);
        /// <summary>
        /// アルファベットをパースする
        /// </summary>
        /// <returns>成功：this, 失敗：NullObject</returns>
        IParser Alpha();
        /// <summary>
        /// 数字をパースする
        /// </summary>
        /// <returns>成功：this, 失敗：NullObject</returns>
        IParser Num();
        /// <summary>
        /// アルファベットと数字をパースする
        /// </summary>
        /// <returns>成功：this, 失敗：NullObject</returns>
        IParser AlphaNum();

        #region AnyCharOtherThanメソッド

        /// <summary>
        /// 指定した文字以外の文字をパースする
        /// </summary>
        /// <param name="c">除外したい文字</param>
        /// <returns>成功：this, 失敗：NullObject</returns>
        IParser AnyCharOtherThan(char c);
        /// <summary>
        /// 指定した文字以外の文字をパースする
        /// </summary>
        /// <param name="c1">除外したい文字</param>
        /// <param name="c2">除外したい文字</param>
        /// <returns>成功：this, 失敗：NullObject</returns>
        IParser AnyCharOtherThan(char c1, char c2);
        /// <summary>
        /// 指定した文字以外の文字をパースする
        /// </summary>
        /// <param name="c1">除外したい文字</param>
        /// <param name="c2">除外したい文字</param>
        /// <param name="c3">除外したい文字</param>
        /// <returns>成功：this, 失敗：NullObject</returns>
        IParser AnyCharOtherThan(char c1, char c2, char c3);
        /// <summary>
        /// 指定した文字以外の文字をパースする
        /// </summary>
        /// <param name="c1">除外したい文字</param>
        /// <param name="c2">除外したい文字</param>
        /// <param name="c3">除外したい文字</param>
        /// <param name="c4">除外したい文字</param>
        /// <returns>成功：this, 失敗：NullObject</returns>
        IParser AnyCharOtherThan(char c1, char c2, char c3, char c4);
        /// <summary>
        /// 指定した文字以外の文字をパースする
        /// </summary>
        /// <param name="clist">除外したい文字を集めた配列</param>
        /// <returns>成功：this, 失敗：NullObject</returns>
        IParser AnyCharOtherThan(char[] clist);
        /// <summary>
        /// 空文字列をパースするεを表す。
        /// なのでスペースなどをパースするものではない。
        /// </summary>
        /// <returns>this</returns>
        IParser Epsilon();
        #endregion
        #endregion

        #endregion
    }
}
