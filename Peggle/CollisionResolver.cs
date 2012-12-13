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
        Dictionary<IEntityPhysics, PolarCoordinate> initalAngles = new Dictionary<IEntityPhysics, PolarCoordinate>();

        public CollisionResolver()
            : base(Game1.game)
        {
            EventHandlers.collision += collisionEventHandler;
        }

        public override void Update(GameTime gameTime)
        {
            initalAngles.Clear();
        }

        public void collisionEventHandler(object sender, CollisionArgs e)
        {
            IEntityPhysics collidingObject = e.collidingObject;

            PolarCoordinate collidingObjectPolar = collidingObject.velocity.toPolar();

            float newOrigin;
            if (!initalAngles.ContainsKey(collidingObject))
            {
                initalAngles.Add(collidingObject, collidingObjectPolar);
                newOrigin = bounceAngle(collidingObjectPolar.origin, e.hitObjectAngle);
            }
            else
            {
                float additionalOrigin = bounceAngle(initalAngles[collidingObject].origin, e.hitObjectAngle);
                newOrigin = e.collidingObject.velocity.toPolar().origin;
            }

            //This makes the way the ball bounces nondeterminate but I want the simulated balls to
            //bounce in the middle of the range that it could bounce.
            if (e.collidingObject is Ball && !((Ball)e.collidingObject).isSimulation)
            {
               // newOrigin += MyMathHelper.shiftRange(-(MathHelper.Pi / 8), (MathHelper.Pi / 8), RandomHelper.randomNormalDistributedFloat());
            }

            e.collidingObject.boundingBox().translate(new PolarCoordinate(e.penetration, newOrigin).toCartesian());

            float newRadius = collidingObjectPolar.radius;

            if (newRadius > 1f)
            {
                newRadius /= PhysicsSettings.COLLISION_SPEED_DIVISOR;
            }
            
            collidingObject.velocity = new PolarCoordinate(newRadius, newOrigin).toCartesian();

        }

        private static float bounceAngle(float collidingAngle, float hitAngle)
        {
            float betweenAngle = MyMathHelper.angleBetween(collidingAngle, hitAngle);

            hitAngle += MathHelper.Pi;

            //if (collidingAngle > MathHelper.PiOver2)
            //{
            //    hitAngle -= betweenAngle;
            //}
            //else
            //{
            //    hitAngle += betweenAngle;
            //}

            //Debug.WriteLine(hitAngle + " " + collidingAngle + " " + betweenAngle);

            return hitAngle;
        }


    }
}
