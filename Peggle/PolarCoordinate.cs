using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Peggle
{
    public struct PolarCoordinate
    {
        public float radius { private set; get; }
        public float origin { private set; get; }

        public PolarCoordinate(float radius, float origin)
            : this()
        {
            this.radius = radius;
            this.origin = origin;
        }

        public Vector2 toCartesian()
        {
            float x = (float)(radius * Math.Cos(origin));
            float y = (float)(radius * Math.Sin(origin));
            return new Vector2(x, y);
        }

        public override string ToString()
        {
            return "r: " + radius + " θ: " + origin;
        }


    }
}
