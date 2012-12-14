using System;
using System.Globalization;
using Microsoft.Xna.Framework;

namespace Helper
{
    static class StringHelper
    {
        public static NumberFormatInfo numberFormat = new NumberFormatInfo();

        static StringHelper()
        {
            numberFormat.NumberDecimalSeparator = ".";
            numberFormat.NumberGroupSeparator = "";
        }


        public static float toFloat(this String floatString)
        {
            return Convert.ToSingle(floatString, numberFormat);
        }

        public static byte toByte(this String floatString)
        {
            return Convert.ToByte(floatString, numberFormat);
        }


        //String expected in form x,y
        public static Vector2 toVector(this String vectorString)
        {
            String[] split = vectorString.Split(',');
            return new Vector2(split[0].toFloat(), split[1].toFloat());
        }
    }
}
