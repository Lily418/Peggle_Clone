﻿using System;
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

            e.collidingObject.location.topLeft += new PolarCoordinate(e.penetration, newOrigin).toCartesian();

            float newRadius = collidingObjectPolar.radius;

<<<<<<< HEAD
            if (newRadius > 2f)
=======
            if (newRadius > 0.8f)
>>>>>>> parent of df1756d... Commit prior to trying something
            {
                newRadius /= PhysicsSettings.COLLISION_SPEED_DIVISOR;
            }
            else
            {
                //newRadius = 0f;
            }
            
            collidingObject.velocity = new PolarCoordinate(newRadius, newOrigin).toCartesian();

        }

        private static float bounceAngle(float collidingAngle, float hitAngle)
        {
            float betweenAngle = MyMathHelper.angleBetween(collidingAngle, hitAngle);

<<<<<<< HEAD
            if (betweenAngle < 3f)
=======
            Debug.WriteLine("Collding Angle " + collidingAngle + " Hit Angle:" + hitAngle + " Between Angle: " + betweenAngle);

            if (betweenAngle < 0.3)
>>>>>>> parent of df1756d... Commit prior to trying something
            {
                betweenAngle *= betweenAngle;
            }
            

            
           

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
