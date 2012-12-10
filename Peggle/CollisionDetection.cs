using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;
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

                foreach (Entity otherEntity in Game1.getComponents().OfType<Entity>())
                {

                    if (!moveableEntity.Equals(otherEntity))
                    {
                        if (checkCollisions(moveableEntity, otherEntity))
                        {
                            continue;
                        }
                    }
                }

            }
        }


        static bool checkCollisions(IEntityPhysics moveableEntity, Entity otherEntity)
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
                    return true;
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
                        return true;
                    }
                }
            }
            else
            {
                Debug.Assert(false, "No Collision defined for types " + moveableEntityBoundingBox.GetType() + " " + otherEntityBoundingBox.GetType());
            }

            return false;
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
            if (circle.origin.Y - circle.radius > quad.maxY
                || circle.origin.Y + circle.radius < quad.minY
                || circle.origin.X + circle.radius < quad.minX
                || circle.origin.X - circle.radius > quad.maxX)
            {
                return new KeyValuePair<float?,float>(null, 0f);
            }

            float? collisionAngle = null;
            float penetration = 0f;

            float? collisionAmount;

            const float COLLISION_THRESHOLD = 1f;

            if ((collisionAmount = lineCircleCollision(quad.topLeft, quad.topRight, circle)) != null && collisionAmount > COLLISION_THRESHOLD)
            {
                initToZero(ref collisionAngle);
                collisionAngle += MyMathHelper.angleBetween(quad.topLeft, quad.topRight);

                if (collisionAmount > penetration)
                {
                    penetration = (float)collisionAmount;
                }
            }
            else if ((collisionAmount = lineCircleCollision(quad.bottomLeft, quad.bottomRight, circle)) != null && collisionAmount > COLLISION_THRESHOLD)
            {
                collisionAngle += MyMathHelper.angleBetween(quad.bottomLeft, quad.bottomRight) - MathHelper.Pi;
                if (collisionAmount > penetration)
                {
                    penetration = (float)collisionAmount;
                }
            }

            if ((collisionAmount = lineCircleCollision(quad.topLeft, quad.bottomLeft, circle)) != null && collisionAmount > COLLISION_THRESHOLD)
            {
                collisionAngle += MyMathHelper.angleBetween(quad.topLeft, quad.bottomLeft);

                if (collisionAmount > penetration)
                {
                    penetration = (float)collisionAmount;
                }
            }
            else if ((collisionAmount = lineCircleCollision(quad.topRight, quad.bottomRight, circle)) != null && collisionAmount > COLLISION_THRESHOLD)
            {
                collisionAngle += MyMathHelper.angleBetween(quad.topRight, quad.bottomRight);

                if (collisionAmount > penetration)
                {
                    penetration = (float)collisionAmount;
                }
            }
            

            return new KeyValuePair<float?, float>(collisionAngle, penetration);
        }

        static void initToZero(ref float? f)
        {
            if (f == null)
            {
                f = 0f;
            }
        }

        static float? lineCircleCollision(Vector2 a, Vector2 b, Circle circle)
        {
            Vector2 closestPointToCircleOnLine = ShapeHelper.getClosestPoint(a, b, circle.origin);

            float distance = Vector2.Distance(circle.origin, closestPointToCircleOnLine);

            if (distance < circle.radius)
            {
                return MathHelper.Distance(distance, circle.radius);
            }
            else
            {
                return null;
            }
        }
    }
}
