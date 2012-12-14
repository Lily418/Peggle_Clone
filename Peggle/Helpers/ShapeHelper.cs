using System;
using Microsoft.Xna.Framework;

namespace Helper
{
    static class ShapeHelper
    {
        //Converted from some Ruby code here http://stackoverflow.com/questions/3120357/get-closest-point-to-a-line
        public static Vector2 getClosestPoint(Vector2 A, Vector2 B, Vector2 P)
        {
            Vector2 a_to_p = new Vector2(P.X - A.X, P.Y - A.Y);     // Storing vector A->P
            Vector2 a_to_b = new Vector2(B.X - A.X, B.Y - A.Y);     // Storing vector A->B

            //Basically finding the squared magnitude of a_to_b
            float atb2 = (float)Math.Pow(a_to_b.X, 2) + (float)Math.Pow(a_to_b.Y, 2);
            
            float atp_dot_atb = Vector2.Dot(a_to_b, a_to_p); // The dot product of a_to_p and a_to_b

            float t = atp_dot_atb / atb2;              // The normalized "distance" from a to your closest point

            Vector2 closestPointOnLine =  new Vector2(x: A.X + a_to_b.X * t, y: A.Y + a_to_b.Y * t);

            //The function was originally designed to find a point along a line, but I'm only interested in the line segment representing
            //the quad's edge. Which is why I'm clamping the values.
            closestPointOnLine.X = MathHelper.Clamp(closestPointOnLine.X, Math.Min(A.X, B.X), Math.Max(A.X, B.X));
            closestPointOnLine.Y = MathHelper.Clamp(closestPointOnLine.Y, Math.Min(A.Y, B.Y), Math.Max(A.Y, B.Y));

            return closestPointOnLine;
        }

    }
}
