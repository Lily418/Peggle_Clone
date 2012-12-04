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
                    EventHandlers.raiseEvent(new CollisionArgs(moveableEntity, null, Vector2.Zero, MathHelper.Pi, 0), EventType.Collision);
                }
                else if (moveableEntity.location.Right > Game1.graphics.GraphicsDevice.Viewport.Width)
                {
                    EventHandlers.raiseEvent(new CollisionArgs(moveableEntity, null, Vector2.Zero, 0, 0), EventType.Collision);
                }

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
                                float hitAngle = Math.Abs(MyMathHelper.angleBetween(otherEntityCircle.origin, movingEntityCircle.origin));
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
                                if (collision(quad, movingEntityCircle))
                                {
                                    Vector2[] closestPointOnEachQuadLine = new Vector2[]{ShapeHelper.getClosestPoint(quad.topLeft, quad.topRight, movingEntityCircle.origin),
                                                                                         ShapeHelper.getClosestPoint(quad.topLeft, quad.bottomLeft, movingEntityCircle.origin),
                                                                                         ShapeHelper.getClosestPoint(quad.bottomLeft, quad.bottomRight, movingEntityCircle.origin),
                                                                                         ShapeHelper.getClosestPoint(quad.topRight, quad.bottomRight, movingEntityCircle.origin)};

                                    Vector2 closestPoint = VectorHelper.getClosestVector(movingEntityCircle.origin, closestPointOnEachQuadLine); 
                                    float hitAngle = Math.Abs(MyMathHelper.angleBetween(closestPoint, movingEntityCircle.origin));
                                    EventHandlers.raiseEvent(new CollisionArgs(moveableEntity, otherEntity, Vector2.Zero, hitAngle, 0f), EventType.Collision);
                                }
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

        static bool collision(Quad quad, Circle circle)
        {
            if (quad.pointInQuad((int)circle.origin.X, (int)circle.origin.Y))
            {
                quad.color = Color.Red;
                return true;
            }

           // return lineCircleCollision(quad.topLeft, quad.topRight, circle) || lineCircleCollision(quad.topLeft, quad.bottomLeft, circle)
           //     || lineCircleCollision(quad.topRight, quad.bottomRight, circle) || lineCircleCollision(quad.bottomLeft, quad.bottomRight, circle);


            if (lineCircleCollision(quad.topLeft, quad.topRight, circle)
                || lineCircleCollision(quad.topLeft, quad.bottomLeft, circle)
                || lineCircleCollision(quad.topRight, quad.bottomRight, circle)
                 || lineCircleCollision(quad.bottomLeft, quad.bottomRight, circle))
            {
                quad.color = Color.Red;
                return true;
            }

            quad.color = Color.Green;
            return false;
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
