using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Peggle
{
    interface IEntityPhysics : Entity
    {
        Vector2 velocity { get; set; }
        float maxSpeed { get; set; }
    }
}
