using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Peggle
{
    static class RandomHelper
    {
        static Random random = new Random();

        public static Random getRandom()
        {
            return random;
        }

        public static Color randomColor()
        {
            switch (random.Next(4))
            {
                case 0: return Color.Red;
                case 1: return Color.Blue;
                case 2: return Color.Green;
                default: return Color.Yellow;
            }
        }

        public static byte randomByte()
        {
            return (byte)random.Next(256);
        }

        public static float randomFloat(float min, float max)
        {
            return (float)random.NextDouble() * (max - min) + min;
        }

        


    }
}
