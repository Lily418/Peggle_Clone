using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Peggle
{
    public struct Location
    {
        public float x { private set; get; }
        public float y { private set; get; }
        public float width { private set; get; }
        public float height { private set; get; }
        public float angle { private set; get; }

        //Calling the this() constructor initializes the fields so they can be assigned
        public Location(float x, float y, float height, float width) : this()
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.angle = 0;
        }

        public Location(float x, float y, float height, float width, float angle) : this(x,y,height,width)
        {
            this.angle = angle;
        }

        public Rectangle asRectangle()
        {
            return new Rectangle((int)x, (int)y, (int)width, (int)height);
        }

    }
}
