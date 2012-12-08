using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Helper
{
    static class MyMathHelper
    {

        public static float angleBetween(float a, float b)
        {
            //new Vector2( cos( angle ), sin ( angle ) )

            a = MathHelper.WrapAngle(a);
            b = MathHelper.WrapAngle(b);

            Vector2 vectorA = new Vector2((float)Math.Cos(a), (float)Math.Sin(a));
            Vector2 vectorB = new Vector2((float)Math.Cos(b), (float)Math.Sin(b));

            //angle_between = acos( Dot( A.normalized, B.normalized ) )
            return (float)Math.Acos(Vector2.Dot(vectorA, vectorB));
        }

        public static float angleBetween(Vector2 a, Vector2 b)
        {
            return (float)Math.Atan2(a.Y - b.Y, a.X - b.X);
        }

        public static float difference(float a, float b)
        {
            return Math.Abs(a - b);
        }

        public static float min(float[] array)
        {
            float min = float.PositiveInfinity;

            foreach (float f in array)
            {
                if (f < min)
                {
                    min = f; 
                }
            }

            return min;
        }

        public static float max(float[] array)
        {
            float max = float.NegativeInfinity;

            foreach (float f in array)
            {
                if (f > max)
                {
                    max = f;
                }
            }

            return max;
        }

        public static float shiftRange(float min, float max, float rangePosition)
        {
            return rangePosition * (max - min) + min;
        }
    }
}
