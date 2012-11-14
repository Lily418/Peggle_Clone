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
            float newRadius = collidingObjectPolar.radius / 1.1f;
            collidingObject.velocity = new PolarCoordinate(newRadius, newOrigin).toCartesian();

        }

        private static float reflectAngle(float collidingAngle, float hitAngle)
        {
            float reverseHitAngle = hitAngle - MathHelper.Pi;
            float betweenHitAngle = MyMathHelper.angleBetween(collidingAngle, hitAngle);
            float betweenReverseHitAngle = MyMathHelper.angleBetween(collidingAngle, reverseHitAngle);


            Debug.WriteLine("Colliding Angle: " + collidingAngle);
            Debug.WriteLine("Hit Angle: " + hitAngle);
            Debug.WriteLine("Reverse Hit Angle" + reverseHitAngle);
            Debug.WriteLine("Between Hit Angle" + betweenHitAngle);
            Debug.WriteLine("Between Reverse Hit Angle" + betweenReverseHitAngle);

            ////Debugger.Break();

            //if (collidingAngle > MathHelper.PiOver2)
            //{
            //    Debug.WriteLine("Colliding Angle more than PI");
            //    return collidingAngle + hitAngle;
            //}
            //else
            //{
            //    Debug.WriteLine("Colliding Angle less than PI");
            //    return collidingAngle - hitAngle;
            //}

            return hitAngle += MathHelper.Pi;
        }


    }
}
