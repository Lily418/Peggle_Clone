using System;
using Microsoft.Xna.Framework;
using Helper;
using System.Diagnostics;

namespace Peggle
{
    class CollisionResolver : GameComponent
    {
        public CollisionResolver()
            : base(Game1.game)
        {
            EventHandlers.getInstance().collision += collisionEventHandler;
        }

        public void collisionEventHandler(object sender, CollisionArgs e)
        {
            IEntityPhysics collidingObject = e.collidingObject;

            PolarCoordinate collidingObjectPolar = collidingObject.velocity.toPolar();

            float newOrigin = bounceAngle(collidingObjectPolar.origin, e.hitObjectAngle);

            e.collidingObject.boundingBox().translate(new PolarCoordinate(e.penetration, newOrigin).toCartesian());

            float newRadius = collidingObjectPolar.radius;

            newRadius /= PhysicsSettings.COLLISION_SPEED_DIVISOR;

            collidingObject.velocity = new PolarCoordinate(newRadius, newOrigin).toCartesian();

        }

        private static float bounceAngle(float collidingAngle, float hitAngle)
        {
            hitAngle += MathHelper.Pi;
            return hitAngle;
        }


    }
}
