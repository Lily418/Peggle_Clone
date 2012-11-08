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

            Debug.WriteLine(newOrigin);

            collidingObject.velocity = new PolarCoordinate(collidingObjectPolar.radius, newOrigin).toCartesian();

        }

        private static float reflectAngle(float collidingAngle, float hitAngle)
        {
            float reverseHitAngle = hitAngle - MathHelper.Pi;


            float betweenHitAngle = angleBetween(collidingAngle, hitAngle);
            float betweenReverseHitAngle = angleBetween(collidingAngle, reverseHitAngle);

            if (betweenHitAngle < betweenReverseHitAngle)
            {
                Debug.WriteLine("Between Hit Angle Smaller");
                return hitAngle - MathHelper.PiOver2;
            }
            else
            {
                Debug.WriteLine("Other Angle");
                return hitAngle + MathHelper.PiOver2;
            }

        }

        private static float angleBetween(float a, float b)
        {
            //new Vector2( cos( angle ), sin ( angle ) )
            
            a = MathHelper.WrapAngle(a);
            b = MathHelper.WrapAngle(b);

            Vector2 vectorA = new Vector2((float)Math.Cos(a), (float)Math.Sin(a));
            Vector2 vectorB = new Vector2((float)Math.Cos(b), (float)Math.Sin(b));

            //angle_between = acos( Dot( A.normalized, B.normalized ) )
            return (float)Math.Acos(Vector2.Dot(vectorA, vectorB));
        }
    }
}
