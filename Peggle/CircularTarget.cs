using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Helper;

namespace Peggle
{
    class CircularTarget : Target, IShallowClone<CircularTarget>
    {
        public Circle location { get; private set; }

        public CircularTarget(Circle location, Color color)
            : base(color)
        {
            this.location = location;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            location.draw(color);    
        }

        public override Shape boundingBox()
        {
            return location;
        }

        public CircularTarget clone()
        {
            return new CircularTarget(location, defaultColor);
        }
    }
}
