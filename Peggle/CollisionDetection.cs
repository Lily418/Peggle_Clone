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
                if (moveableEntity.location.Left < 0)
                {
                    EventHandlers.raiseEvent(new CollisionArgs(moveableEntity, null, Vector2.Zero, MathHelper.Pi, MathHelper.Distance(0, moveableEntity.location.Left)), EventType.Collision);
                }
                else if (moveableEntity.location.Right > Game1.graphics.GraphicsDevice.Viewport.Width)
                {
                    EventHandlers.raiseEvent(new CollisionArgs(moveableEntity, null, Vector2.Zero, 0, MathHelper.Distance(Game1.graphics.GraphicsDevice.Viewport.Width, moveableEntity.location.Right)), EventType.Collision);
                }

                bool curveCollisionFound = false;
                foreach (Entity otherEntity in Game1.getComponents().OfType<Entity>())
                {

                    if (!moveableEntity.Equals(otherEntity))
                    {
                        Shape moveableEntityBoundingBox = moveableEntity.boundingBox();
                        Shape otherEntityBoundingBox = otherEntity.boundingBox();

                        if (moveableEntityBoundingBox is Circle && otherEntityBoundingBox is Circle)
                        {
                            Circle movingEntityCircle = (Circle)moveableEntityBoundingBox;
                            Circle otherEntityCircle = (Circle)otherEntityBoundingBox;

                            float penetration;
                            if ((penetration = collision(movingEntityCircle, otherEntityCircle)) > 0)
                            {
                                float hitAngle = MyMathHelper.angleBetween(otherEntityCircle.origin, movingEntityCircle.origin);
                                EventHandlers.raiseEvent(new CollisionArgs(moveableEntity, otherEntity, Vector2.Zero, hitAngle, penetration), EventType.Collision);
                            }
                        }
                        else if (moveableEntityBoundingBox is Circle && otherEntityBoundingBox is QuadCollection)
                        {
                            Circle movingEntityCircle = (Circle)moveableEntityBoundingBox;
                            QuadCollection quadCollection = (QuadCollection)otherEntityBoundingBox;
                            List<Quad> quads = quadCollection.quads;

                            foreach (Quad quad in quads)
                            {
                                float? collisionAngle;
                                if ((collisionAngle = collision(quad, movingEntityCircle)).HasValue)
                                {

                                    //float hitAngle = MyMathHelper.angleBetween(collisionLine.Value.Key, collisionLine.Value.Value) - MathHelper.PiOver2;
                                    EventHandlers.raiseEvent(new CollisionArgs(moveableEntity, otherEntity, Vector2.Zero, (float)collisionAngle, 0f), EventType.Collision);
                                    curveCollisionFound = true;
                                    break;
                                }
                            }

                            if (curveCollisionFound)
                            {
                                break;
                            }
                        }
                        else
                        {
                            Debug.Assert(false, "No Collision defined for types " + moveableEntityBoundingBox.GetType() + " " + otherEntityBoundingBox.GetType());
                        }
                    }
                }


            }
        }

        static float collision(Circle one, Circle two)
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

        static float? collision(Quad quad, Circle circle)
        {
            float? collisionAngle = null;
            if (lineCircleCollision(quad.topLeft, quad.topRight, circle))
            {
                initToZero(ref collisionAngle);
                Debug.WriteLine("Top Collision" + MyMathHelper.angleBetween(quad.topLeft, quad.topRight));
                quad.color = Color.Red;
                collisionAngle += MyMathHelper.angleBetween(quad.topLeft, quad.topRight);
            }
            
            if (lineCircleCollision(quad.topLeft, quad.bottomLeft, circle))
            {
                initToZero(ref collisionAngle);
                Debug.WriteLine("Left Collision");
                quad.color = Color.Red;
                collisionAngle += MyMathHelper.angleBetween(quad.topLeft, quad.bottomLeft);
            }
            
            if (lineCircleCollision(quad.topRight, quad.bottomRight, circle))
            {
                initToZero(ref collisionAngle);
                Debug.WriteLine("Right Collision" + MyMathHelper.angleBetween(quad.topRight, quad.bottomRight));
                quad.color = Color.Red;
                collisionAngle += MyMathHelper.angleBetween(quad.topRight, quad.bottomRight);
            }
            
            if (lineCircleCollision(quad.bottomLeft, quad.bottomRight, circle))
            {
                initToZero(ref collisionAngle);
                Debug.WriteLine("Bottom Collision");
                quad.color = Color.Red;
                collisionAngle += MyMathHelper.angleBetween(quad.bottomLeft, quad.bottomRight);
            }

            if (collisionAngle == null)
            {
                quad.color = Color.Green;
            }

            return collisionAngle;
        }

        private static void initToZero(ref float? x)
        {
            if (x == null)
            {
                x = 0f;
            }
        }

        static bool lineCircleCollision(Vector2 a, Vector2 b, Circle circle)
        {
            Vector2 closestPointToCircleOnLine = ShapeHelper.getClosestPoint(a, b, circle.origin);

            float distance = Vector2.Distance(circle.origin, closestPointToCircleOnLine);

            if (distance < circle.radius)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
