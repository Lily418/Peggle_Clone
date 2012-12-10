using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Peggle
{
    public interface Shape
    {
        void translate(Vector2 direction);
        float leftMostPoint();
        float rightMostPoint();
    }
}
