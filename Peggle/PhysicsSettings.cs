using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Peggle
{
    static class PhysicsSettings
    {
            //How much speed is reduced by when a collision occurs
            public const float COLLISION_SPEED_DIVISOR = 1.2f;

            public static readonly Vector2 GRAVITY = new Vector2(0, 0.2f);

            public const float MAX_BALL_SPEED = 8f;
            
    }
}
