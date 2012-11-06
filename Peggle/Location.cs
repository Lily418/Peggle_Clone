using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Peggle
{
    struct Location
    {
        float x;
        float y;
        float width;
        float height;
        float angle;

        public Location(float x, float y, float height, float width)
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

    }
}
