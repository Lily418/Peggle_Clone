using Microsoft.Xna.Framework;
using Helper;

namespace Peggle
{
    class CollisionResolver : GameComponent
    {
        public CollisionResolver()
            : base(Game1.game)
        {
            EventHandlers.collision += collisionEventHandler;
        }

        public void collisionEventHandler(object sender, CollisionArgs e)
        {
            IEntityPhysics collidingObject = e.collidingObject;

            PolarCoordinate collidingObjectPolar = collidingObject.velocity.toPolar();

            float newOrigin = bounceAngle(collidingObjectPolar.origin, e.hitObjectAngle);

            //This makes the way the ball bounces nondeterminate but I want the simulated balls to
            //bounce in the middle of the range that it could bounce.
            if (e.collidingObject is Ball && !((Ball)e.collidingObject).isSimulation)
            {
                newOrigin += MyMathHelper.shiftRange(-(MathHelper.Pi / 8), (MathHelper.Pi / 8), RandomHelper.randomNormalDistributedFloat());
            }

            e.collidingObject.boundingBox().translate(new PolarCoordinate(e.penetration, newOrigin).toCartesian());

            float newRadius = collidingObjectPolar.radius;

            if (newRadius > 2f)
            {
                newRadius /= PhysicsSettings.COLLISION_SPEED_DIVISOR;
            }

            collidingObject.velocity = new PolarCoordinate(newRadius, newOrigin).toCartesian();

        }

        private static float bounceAngle(float collidingAngle, float hitAngle)
        {
            float betweenAngle = MyMathHelper.angleFromAToB(collidingAngle, hitAngle);

            hitAngle += MathHelper.Pi;

            if (collidingAngle > MathHelper.PiOver2)
            {
                hitAngle -= betweenAngle;
            }
            else
            {
                hitAngle += betweenAngle;
            }

            return hitAngle;
        }


    }
}
