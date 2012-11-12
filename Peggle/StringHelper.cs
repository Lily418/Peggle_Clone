using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Peggle
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
    }
}
