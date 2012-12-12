﻿//#define DEBUG_MODE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Helper;
using System.Threading;
using Microsoft.Xna.Framework.Input;

namespace Peggle
{
    static class CollisionDetection
    {
        public static void checkCollisions()
        {
            foreach (IEntityPhysics moveableEntity in Game1.getComponents().OfType<IEntityPhysics>())
            {
                if (wallCollision(moveableEntity))
                {
                    continue;
                }

                foreach (Entity otherEntity in Game1.getComponents().OfType<Entity>())
                {

                    if (!moveableEntity.Equals(otherEntity))
                    {
                        checkCollisions(moveableEntity, otherEntity);
                    }
                }

            }
        }


        static void checkCollisions(IEntityPhysics moveableEntity, Entity otherEntity)
        {
            Shape moveableEntityBoundingBox = moveableEntity.boundingBox();
            Shape otherEntityBoundingBox = otherEntity.boundingBox();

            if (moveableEntityBoundingBox is Circle && otherEntityBoundingBox is Circle)
            {
                Circle movingEntityCircle = (Circle)moveableEntityBoundingBox;
                Circle otherEntityCircle = (Circle)otherEntityBoundingBox;

                float penetration;
                if ((penetration = circleCollision(movingEntityCircle, otherEntityCircle)) > 0)
                {
                    float hitAngle = MyMathHelper.angleBetween(otherEntityCircle.origin, movingEntityCircle.origin);
                    EventHandlers.raiseEvent(new CollisionArgs(moveableEntity, otherEntity, Vector2.Zero, hitAngle, penetration));
                }
            }
            else if (moveableEntityBoundingBox is Circle && otherEntityBoundingBox is QuadCollection)
            {
                Circle movingEntityCircle = (Circle)moveableEntityBoundingBox;
                QuadCollection quadCollection = (QuadCollection)otherEntityBoundingBox;
                List<Quad> quads = quadCollection.quads;

                foreach (Quad quad in quads)
                {
                    KeyValuePair<float?, float> collisionAngle;

                    if ((collisionAngle = quadCircleCollision(quad, movingEntityCircle)).Key.HasValue)
                    {
                        EventHandlers.raiseEvent(new CollisionArgs(moveableEntity, otherEntity, Vector2.Zero, (float)collisionAngle.Key, collisionAngle.Value));
                    }
                }
            }
            else
            {
                Debug.Assert(false, "No Collision defined for types " + moveableEntityBoundingBox.GetType() + " " + otherEntityBoundingBox.GetType());
            }
        }

        static bool wallCollision(IEntityPhysics moveableEntity)
        {
            if (moveableEntity.boundingBox().leftMostPoint() < 0)
            {
                EventHandlers.raiseEvent(new CollisionArgs(moveableEntity, null, Vector2.Zero, MathHelper.Pi, MathHelper.Distance(0, moveableEntity.boundingBox().leftMostPoint())));
                return true;

            }
            else if (moveableEntity.boundingBox().rightMostPoint() > Game1.graphics.GraphicsDevice.Viewport.Width)
            {
                EventHandlers.raiseEvent(new CollisionArgs(moveableEntity, null, Vector2.Zero, 0, MathHelper.Distance(Game1.graphics.GraphicsDevice.Viewport.Width, moveableEntity.boundingBox().rightMostPoint())));
                return true;
            }
            else
            {
                return false;
            }
        }

        static float circleCollision(Circle one, Circle two)
        {
            float distance = Vector2.Distance(one.origin, two.origin);

            if (distance < one.radius + two.radius)
            {
                return one.radius + two.radius - distance;
            }
            else
            {
                return 0;
            }

        }

        private static KeyValuePair<float?, float> quadCircleCollision(Quad quad, Circle circle)
        {
            if  (circle.origin.Y - circle.radius > quad.maxY
                || circle.origin.Y + circle.radius < quad.minY
                || circle.origin.X + circle.radius < quad.minX
                || circle.origin.X - circle.radius > quad.maxX)
            {
                return new KeyValuePair<float?,float>(null, 0f);
            }

            float? collisionAngle = null;
            float penetration = 0f;

            float? collisionAmount;

            const float COLLISION_THRESHOLD = 2f;

            if ((collisionAmount = lineCircleCollision(quad.topLeft, quad.topRight, circle)) != null && collisionAmount > COLLISION_THRESHOLD)
            {
                MyMathHelper.angleBetweenAngles(ref collisionAngle, MathHelper.WrapAngle(MyMathHelper.angleBetween(quad.topLeft, quad.topRight) - MathHelper.PiOver2));

#if DEBUG_MODE
                Debug.WriteLine("Top" + collisionAmount);
#endif
                if (collisionAmount > penetration)
                {
                    penetration += (float)collisionAmount;
                }
            }
            else if ((collisionAmount = lineCircleCollision(quad.bottomLeft, quad.bottomRight, circle)) != null && collisionAmount > COLLISION_THRESHOLD)
            {
#if DEBUG_MODE
                Debug.WriteLine("Bottom" + collisionAmount);
#endif
                MyMathHelper.angleBetweenAngles(ref collisionAngle, MyMathHelper.angleBetween(quad.bottomLeft, quad.bottomRight) + MathHelper.Pi); 
                if (collisionAmount > penetration)
                {
                    penetration += (float)collisionAmount;
                }
            }
            else if ((collisionAmount = lineCircleCollision(quad.topLeft, quad.bottomLeft, circle)) != null && collisionAmount > COLLISION_THRESHOLD)
            {
#if DEBUG_MODE
                Debug.WriteLine("Left" + collisionAmount);
#endif
                MyMathHelper.angleBetweenAngles(ref collisionAngle, MyMathHelper.angleBetween(quad.topLeft, quad.bottomLeft) + MathHelper.PiOver2);

                if (collisionAmount > penetration)
                {
                    penetration += (float)collisionAmount;
                }
            }
            else if ((collisionAmount = lineCircleCollision(quad.topRight, quad.bottomRight, circle)) != null && collisionAmount > COLLISION_THRESHOLD)
            {
#if DEBUG_MODE
                Debug.WriteLine("Right" + collisionAmount);
#endif

                MyMathHelper.angleBetweenAngles(ref collisionAngle, MyMathHelper.angleBetween(quad.topRight, quad.bottomRight) + MathHelper.PiOver2);

                if (collisionAmount > penetration)
                {
                    penetration += (float)collisionAmount;
                }
            }


#if DEBUG_MODE
            //Debug.WriteLine("Final Collision Angle" + collisionAngle);
#endif

            return new KeyValuePair<float?, float>(collisionAngle, penetration);
        }


        static float? lineCircleCollision(Vector2 a, Vector2 b, Circle circle)
        {
            Vector2 closestPointToCircleOnLine = ShapeHelper.getClosestPoint(a, b, circle.origin);

            float distance = Vector2.Distance(circle.origin, closestPointToCircleOnLine);

            if (distance < circle.radius)
            {
                return Vector2.Distance(closestPointToCircleOnLine, circle.origin);
            }
            else
            {
                return null;
            }
        }
    }
}
