using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Helper
{
    static class ColorHelper
    {
        public static Color increaseBrightness(this Color color, byte amount)
        {
            Color newColor = new Color(color.R + amount, color.G + amount, color.B + amount);

            newColor.R = unwrapOverflowedByte(color.R, newColor.R);
            newColor.B = unwrapOverflowedByte(color.B, newColor.B);
            newColor.G = unwrapOverflowedByte(color.G, newColor.G);

            return newColor;
            
        }

        private static byte unwrapOverflowedByte(byte oldByte, byte newByte)
        {
            if (newByte < oldByte)
            {
                newByte = 255;
            }

            return newByte;
        }

        public static Color parseColorString(String colorString)
        {
            String[] splitString = colorString.Split(',');
            return new Color(splitString[0].toByte(), splitString[1].toByte(), splitString[2].toByte());
        }
    }
}
