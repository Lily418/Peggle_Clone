using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Peggle;

namespace Helper
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

        public static Vector2 getClosestVector(Vector2 point, Vector2[] points)
        {
            //I decided not to write the method in a way that assumes points has at least one element
            //I didn't want to return a magic number/meaningless value
            //I considered making the return type a nullable Vector2 but decided this could lead to NullReference exceptions
            //when the method was misused, which would be harder to debug than an implicit exception
            if (points.Length == 0)
            {
                throw new ArgumentException("Points cannot be empty");
            }

            Vector2 closest = Vector2.Zero;
            float currentMinDistance = float.PositiveInfinity;
            foreach (Vector2 vector in points)
            {
                float thisDistance;
                if ((thisDistance = Vector2.Distance(closest, vector)) < currentMinDistance)
                {
                    currentMinDistance = thisDistance;
                    closest = vector;
                }
            }

            return closest;
        }

        public static Point toPoint(this Vector2 vector)
        {
            return new Point((int)vector.X, (int)vector.Y);
        }
    }
}
