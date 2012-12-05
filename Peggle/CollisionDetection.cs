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
                                KeyValuePair<Vector2, Vector2>? collisionLine;
                                if ((collisionLine = collision(quad, movingEntityCircle)).HasValue)
                                {

                                    float hitAngle = MyMathHelper.angleBetween(collisionLine.Value.Key, collisionLine.Value.Value) - MathHelper.PiOver2;
                                    EventHandlers.raiseEvent(new CollisionArgs(moveableEntity, otherEntity, Vector2.Zero, hitAngle, 0f), EventType.Collision);
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

        static KeyValuePair<Vector2, Vector2>? collision(Quad quad, Circle circle)
        {
<<<<<<< HEAD
            bool collision = false;
            if (lineCircleCollision(quad.topLeft, quad.topRight, circle))
            {
                quad.color = Color.Red;
                collision = true;
            }
            
            else if (lineCircleCollision(quad.topLeft, quad.bottomLeft, circle))
            {
                quad.color = Color.Red;
                collision = true;
            }
            
            else if (lineCircleCollision(quad.topRight, quad.bottomRight, circle))
            {
                quad.color = Color.Red;
                collision = true;
            }
            
            else if (lineCircleCollision(quad.bottomLeft, quad.bottomRight, circle))
            {
                quad.color = Color.Red;
                collision = true;
            }

            if (!collision)
            {
                quad.color = Color.Green;
                return null;
            }
            else
            {
                if (circle.origin.Y < quad.topLeft.Y || circle.origin.Y < quad.topRight.Y)
                {
                    Debug.WriteLine("Top");
                    return MyMathHelper.angleBetween(quad.topLeft, quad.topRight);
                }
                else if (circle.origin.X < quad.topLeft.X || circle.origin.X < quad.bottomLeft.X)
                {
                    Debug.WriteLine("Left");
                    return MathHelper.PiOver2;
                }
                else if (circle.origin.X > quad.topRight.X || circle.origin.X > quad.bottomRight.X)
                {
                    Debug.WriteLine("Right");
                    return MathHelper.PiOver2;
                }
                else
                {
                    Debug.WriteLine("Bottom");
                    return MyMathHelper.angleBetween(quad.bottomLeft, quad.bottomRight);
                }
            }

        }

        private static void initToZero(ref float? x)
        {
            if (x == null)
            {
                x = 0f;
            }
=======
            if (lineCircleCollision(quad.topLeft, quad.topRight, circle))
            {
                quad.color = Color.Red;
                return new KeyValuePair<Vector2,Vector2>(quad.topLeft, quad.topRight);
            }
            else if(lineCircleCollision(quad.topLeft, quad.bottomLeft, circle))
            {
                quad.color = Color.Red;
                return new KeyValuePair<Vector2,Vector2>(quad.topLeft, quad.bottomLeft);
            }
            else if (lineCircleCollision(quad.topRight, quad.bottomRight, circle))
            {
                quad.color = Color.Red;
                return new KeyValuePair<Vector2,Vector2>(quad.topRight, quad.bottomRight);
            }
            else if(lineCircleCollision(quad.bottomLeft, quad.bottomRight, circle))
            {
                quad.color = Color.Red;
                return new KeyValuePair<Vector2, Vector2>(quad.topLeft, quad.bottomRight);
            }

            quad.color = Color.Green;
            return null;
>>>>>>> parent of df1756d... Commit prior to trying something
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
