using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Peggle;

namespace Helper
{
    class ShapeHelper
    {
        public static bool rayCircleIntersection(Vector2 startRay, Vector2 endRay, Circle circle)
        {
            Vector2 d = endRay - startRay;
            Vector2 f = startRay - circle.origin;

            float a = Vector2.Dot(d, d);
            float b = 2 * Vector2.Dot(f, d);
            float c = Vector2.Dot(f, f) - (float)Math.Pow(circle.radius, 2);

            float discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
            {
                // no intersection
                return false;
            }
            else
            {
                return true;
                //// ray didn't totally miss sphere,
                //// so there is a solution to
                //// the equation.


                //discriminant = (float)Math.Sqrt(discriminant);
                //// either solution may be on or off the ray so need to test both
                //float t1 = (-b + discriminant) / (2 * a);
                //float t2 = (-b - discriminant) / (2 * a);

                //if (t1 >= 0 && t1 <= 1)
                //{
                //    // t1 solution on is ON THE RAY.
                //}
                //else
                //{
                //    // t1 solution "out of range" of ray
                //}

                //if (t2 >= 0 && t2 <= 1)
                //{
                //    // t2 solution on is ON THE RAY.
                //}
                //else
                //{
                //    // t2 solution "out of range" of ray
                //}
            }
        }
    }
}
