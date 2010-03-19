using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base.ParserCombinators
{
    abstract class ParserCombinators
    {
        protected Stream<T> _input;

        /// <summary>
        /// 引数で与えられたパーサ(メソッド)を複数回適用する。
        /// 正規表現の"*"に相当する。
        /// </summary>
        /// <typeparam name="T">TraceLogではchar</typeparam>
        /// <param name="f">パーサ(メソッド)</param>
        /// <returns>パースした結果を保持したList(Count==0の場合もあり)</returns>
        protected List<T> Many<T>(Func<T> f)
        {
            var xs = new List<T>();

            // fから例外が飛んでくるまでループ
            try
            {
                while( true )
                {
                    xs.Add( f() );

                }
            }
            catch(Exception e)
            {}

            return xs;
        }


        /// <summary>
        /// 引数で与えられたパーサ(メソッド)を1回以上適用する。
        /// 正規表現の"+"に相当する。
        /// </summary>
        /// <typeparam name="T">TraceLogではchar</typeparam>
        /// <param name="f">パーサ(メソッド)</param>
        /// <returns>パースした結果を保持したList(Count>0以外は例外投げ)</returns>
        protected List<T> Many1<T>(Func<T> f)
        {
            List<T> xs = Many<T>( f );

            if (xs.Count > 0)
            {
                return xs;
            }
            else
            {
                throw new Exception();
            }
        }


        protected List<T> trying<S>(Func<S> f) 
            where T : struct
        {
            var state = _input.Save();

            try
 	        {
                return f();
            }
            catch (Exception e)
            {
                _input.Restore(state);
                throw e;
            }
        }
 	
    }
}
