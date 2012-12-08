using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Peggle;

namespace Helper
{
    static class ShapeHelper
    {
        //Converted from some Ruby code here http://stackoverflow.com/questions/3120357/get-closest-point-to-a-line
        public static Vector2 getClosestPoint(Vector2 A, Vector2 B, Vector2 P)
        {
            Vector2 a_to_p = new Vector2(P.X - A.X, P.Y - A.Y);     // Storing vector A->P
            Vector2 a_to_b = new Vector2(B.X - A.X, B.Y - A.Y);     // Storing vector A->B

            float atb2 = (float)Math.Pow(a_to_b.X, 2) + (float)Math.Pow(a_to_b.Y, 2);
            //   Basically finding the squared magnitude
            //   of a_to_b

            float atp_dot_atb = Vector2.Dot(a_to_b, a_to_p); // The dot product of a_to_p and a_to_b

            float t = atp_dot_atb / atb2;              // The normalized "distance" from a to
            //   your closest point

            Vector2 closestPointOnLine =  new Vector2(x: A.X + a_to_b.X * t, y: A.Y + a_to_b.Y * t);

            closestPointOnLine.X = MathHelper.Clamp(closestPointOnLine.X, Math.Min(A.X, B.X), Math.Max(A.X, B.X));
            closestPointOnLine.Y = MathHelper.Clamp(closestPointOnLine.Y, Math.Min(A.Y, B.Y), Math.Max(A.Y, B.Y));

            return closestPointOnLine;
        }

    }
}
