using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Helper;

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

                foreach (IEntity otherEntity in Game1.getComponents().OfType<IEntity>())
                {

                    if (!moveableEntity.Equals(otherEntity))
                    {
                        checkCollisions(moveableEntity, otherEntity);
                    }
                }

            }
        }


        private static void checkCollisions(IEntityPhysics moveableEntity, IEntity otherEntity)
        {
            Shape moveableEntityBoundingBox = moveableEntity.boundingBox();
            Shape otherEntityBoundingBox = otherEntity.boundingBox();

            if (moveableEntityBoundingBox is Circle && otherEntityBoundingBox is Circle)
            {
                Circle movingEntityCircle = (Circle)moveableEntityBoundingBox;
                Circle otherEntityCircle  = (Circle)otherEntityBoundingBox;

                float penetration;
                if ((penetration = circleCollision(movingEntityCircle, otherEntityCircle)) > 0)
                {
                    float hitAngle = MyMathHelper.angleBetween(otherEntityCircle.origin, movingEntityCircle.origin);
                    EventHandlers.raiseEvent(new CollisionArgs(moveableEntity, otherEntity, hitAngle, penetration));
                }
            }
            else if (moveableEntityBoundingBox is Circle && otherEntityBoundingBox is QuadCollection)
            {
                Circle movingEntityCircle = (Circle)moveableEntityBoundingBox;
                QuadCollection quadCollection = (QuadCollection)otherEntityBoundingBox;

                foreach (Quad quad in quadCollection.quads)
                {
                    KeyValuePair<float?, float> collisionAngle;
                    if ((collisionAngle = quadCircleCollision(quad, movingEntityCircle)).Key != null)
                    {
                        EventHandlers.raiseEvent(new CollisionArgs(moveableEntity, otherEntity, (float)collisionAngle.Key, collisionAngle.Value));
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
                EventHandlers.raiseEvent(new CollisionArgs(moveableEntity, null, MathHelper.Pi, MathHelper.Distance(0, moveableEntity.boundingBox().leftMostPoint())));
                return true;
            }
            else if (moveableEntity.boundingBox().rightMostPoint() > Game1.graphics.GraphicsDevice.Viewport.Width)
            {
                EventHandlers.raiseEvent(new CollisionArgs(moveableEntity, null, 0, MathHelper.Distance(Game1.graphics.GraphicsDevice.Viewport.Width, moveableEntity.boundingBox().rightMostPoint())));
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
            if   ( circle.origin.Y - circle.radius > quad.maxY
                || circle.origin.Y + circle.radius < quad.minY
                || circle.origin.X - circle.radius > quad.maxX
                || circle.origin.X + circle.radius < quad.minX)
            {
                return new KeyValuePair<float?,float>(null, 0f);
            }

            const float COLLISION_THRESHOLD = 1f;
            float? collisionAmount;

            if ((collisionAmount = lineCircleCollision(quad.topLeft, quad.topRight, circle)) != null && collisionAmount > COLLISION_THRESHOLD)
            {
                //Debug.WriteLine("Top" + quad.topLeft +" "+quad.topRight)

                return new KeyValuePair<float?, float>(MyMathHelper.angleBetween(quad.topRight, quad.topLeft) - MathHelper.PiOver2, (float)collisionAmount);                
            }
            else if ((collisionAmount = lineCircleCollision(quad.bottomLeft, quad.bottomRight, circle)) != null && collisionAmount > COLLISION_THRESHOLD)
            {
                //Debug.WriteLine("Bottom");
                return new KeyValuePair<float?, float>(MyMathHelper.angleBetween(quad.bottomLeft, quad.bottomRight) + MathHelper.PiOver2, (float)collisionAmount); 

            }
            else if ((collisionAmount = lineCircleCollision(quad.topLeft, quad.bottomLeft, circle)) != null && collisionAmount > COLLISION_THRESHOLD)
            {
                //Debug.WriteLine("Left");
                return new KeyValuePair<float?,float>(MyMathHelper.angleBetween(quad.topLeft, quad.bottomLeft) + MathHelper.PiOver2, (float)collisionAmount);
            }
            else if ((collisionAmount = lineCircleCollision(quad.topRight, quad.bottomRight, circle)) != null && collisionAmount > COLLISION_THRESHOLD)
            {
                //Debug.WriteLine("Right" + MyMathHelper.angleBetween(quad.topRight, quad.bottomRight));
                return new KeyValuePair<float?, float>(MyMathHelper.angleBetween(quad.topRight, quad.bottomRight), (float)collisionAmount);
            }

            return new KeyValuePair<float?, float>(null, 0f);
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
