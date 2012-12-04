using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Helper;

namespace Peggle
{
    class CollisionResolver : GameComponent
    {
        const float BOUNCE_CUTOFF = 0.5f;

        public CollisionResolver(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            EventHandlers.collision += collisionEventHandler;
        }

        public static void collisionEventHandler(object sender, CollisionArgs e)
        {
            IEntityPhysics collidingObject = e.collidingObject;

            PolarCoordinate collidingObjectPolar = collidingObject.velocity.toPolar();
            float newOrigin = bounceAngle(collidingObjectPolar.origin, e.hitObjectAngle);
            float newRadius = collidingObjectPolar.radius - (collidingObjectPolar.radius / 4);

            if (newRadius < 0f)
            {
                newRadius = 0f;
            }
            collidingObject.velocity = new PolarCoordinate(newRadius, newOrigin).toCartesian();

        }

        private static float bounceAngle(float collidingAngle, float hitAngle)
        {
            return hitAngle += MathHelper.Pi;
        }


    }
}
