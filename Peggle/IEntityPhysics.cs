using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Peggle
{
    public interface IEntityPhysics : Entity
    {
        Location location { get; set; }
        Vector2 velocity { get; set; }
        float maxSpeed { get; set; }
    }
}
