using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Peggle
{
    public class Location
    {
        public Vector2 topLeft { set; get; }
        public float width {set; get; }
        public float height {set; get; }
        public float angle {set; get; }
        public float X { get {return topLeft.X; } }
        public float Y { get { return topLeft.Y; } }



        public Location(Vector2 location, float height, float width)
        {
            this.topLeft = location;
            this.width = width;
            this.height = height;
            this.angle = 0;
        }

        public Location(Vector2 location, float height, float width, float angle) : this(location,height,width)
        {
            this.angle = angle;
        }

        public Rectangle asRectangle()
        {
            return new Rectangle((int)X, (int)Y, (int)width, (int)height);
        }

    }
}
