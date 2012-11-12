using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Peggle
{
    class CollisionResolver : GameComponent
    {
        public CollisionResolver(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            EventHanders.collision += collisionEventHandler;
        }

        public static void collisionEventHandler(object sender, CollisionArgs e)
        {
            IEntityPhysics collidingObject = e.collidingObject;

            PolarCoordinate collidingObjectPolar = collidingObject.velocity.toPolar();
            float newOrigin = reflectAngle(collidingObjectPolar.origin, e.hitObjectAngle);
            float newRadius = collidingObjectPolar.radius / 10;

            collidingObject.velocity = new PolarCoordinate(collidingObjectPolar.radius, newOrigin).toCartesian();

        }

        private static float reflectAngle(float collidingAngle, float hitAngle)
        {
            float reverseHitAngle = hitAngle - MathHelper.Pi;


            float betweenHitAngle = MyMathHelper.angleBetween(collidingAngle, hitAngle);
            float betweenReverseHitAngle = MyMathHelper.angleBetween(collidingAngle, reverseHitAngle);

            if (betweenHitAngle < betweenReverseHitAngle)
            {
                return hitAngle;
            }
            else
            {
                return reverseHitAngle;
            }

        }


    }
}
