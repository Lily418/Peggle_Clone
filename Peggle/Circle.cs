using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Peggle
{
    class Circle : Shape
    {
        public Vector2 origin { private set; get; }
        public float radius { private set; get; }

        public Circle(Vector2 origin, float radius)
        {
            this.origin = origin;
            this.radius = radius;
        }

        public static Circle circleFromLocation(Location location)
        {
            return new Circle(location.Center, location.width / 2);
        }
    }
}
