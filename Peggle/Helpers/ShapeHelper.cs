using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Peggle;
using System.Diagnostics;

namespace Helper
{
    static class ShapeHelper
    {
        public static Quad organiseQuadPoints(Vector2[] points)
        {
            Array.Sort(points, new VectorComparer(VectorComparer.Axis.x));

            Vector2 topLeft;
            Vector2 bottomLeft;
            Vector2 topRight;
            Vector2 bottomRight;

            if (points[0].Y < points[1].Y)
            {
                topLeft = points[0];
                bottomLeft = points[1];
            }
            else
            {
                topLeft = points[1];
                bottomLeft = points[0];
            }

            if (points[2].Y < points[3].Y)
            {
                topRight = points[2];
                bottomRight = points[3];
            }
            else
            {
                topRight = points[3];
                bottomRight = points[2];
            }


            return new Quad(topLeft, topRight, bottomLeft, bottomRight);
        }
    }
}
