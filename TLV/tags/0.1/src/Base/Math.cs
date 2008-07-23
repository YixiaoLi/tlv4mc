using System;

namespace NU.OJL.MPRTOS.TLV.Base
{
    public static class CustomMath
    {
        public static double RoundOff(double roundValue, int digits)
        {
            double shift = Math.Pow(10, (double)digits);
            return Math.Floor(roundValue * shift + 0.5) / shift;
        } 
    }
}
