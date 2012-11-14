using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Peggle
{
    //Extension methods for Vector2
    public static class VectorHelper
    {
        public static Vector2 shorten(this Vector2 vector, float maxLength)
        {

            if (vector.Length() > maxLength)
            {
                
                float currentOrigin = vector.toPolar().origin;

                vector = new PolarCoordinate(maxLength, currentOrigin).toCartesian();
            }

            return vector;
        }

        public static PolarCoordinate toPolar(this Vector2 cartesian)
        {

            float r = (float)Math.Sqrt(Math.Pow(cartesian.X, 2) + Math.Pow(cartesian.Y, 2));
            float origin = (float)Math.Atan(cartesian.Y / cartesian.X);

            //Corrects angle returned by Math.Atan
            if (cartesian.X < 0)
            {
                origin += MathHelper.Pi;
            }


            return new PolarCoordinate(r, origin);

        }

        public static Point toPoint(this Vector2 vector)
        {
            return new Point((int)vector.X, (int)vector.Y);
        }
    }
}
