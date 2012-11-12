using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Peggle
{
    static class CollisionDetection
    {
        public static void checkCollisions()
        {
            foreach (IEntityPhysics moveableEntity in Game1.entities.OfType<IEntityPhysics>())
            {
                if (moveableEntity.location.Left < 0)
                {
                    EventHanders.raiseEvent(new CollisionArgs(moveableEntity, float.PositiveInfinity, Vector2.Zero, 0), EventType.Collision);
                }
                else if (moveableEntity.location.Right > Game1.graphics.GraphicsDevice.Viewport.Width)
                {
                    EventHanders.raiseEvent(new CollisionArgs(moveableEntity, float.PositiveInfinity, Vector2.Zero, +MathHelper.Pi), EventType.Collision);
                }

                foreach (Entity otherEntity in Game1.entities.OfType<Entity>())
                {
                    if (!moveableEntity.Equals(otherEntity))
                    {
                        Shape moveableEntityBoundingBox = moveableEntity.boundingBox();
                        Shape otherEntityBoundingBox = otherEntity.boundingBox();

                        if (moveableEntityBoundingBox is Circle && otherEntityBoundingBox is Circle)
                        {
                            Circle movingEntityCircle = (Circle)moveableEntityBoundingBox;
                            Circle otherEntityCircle = (Circle)otherEntityBoundingBox;
                            if (collision(movingEntityCircle, otherEntityCircle))
                            {
                                float hitAngle = Math.Abs(MyMathHelper.angleBetween(movingEntityCircle.origin, otherEntityCircle.origin) + MathHelper.PiOver2);
                                EventHanders.raiseEvent(new CollisionArgs(moveableEntity, float.PositiveInfinity, Vector2.Zero, hitAngle), EventType.Collision); 
                            }
                        }
                        else
                        {
                            Debug.Assert(false, "No Collision defined for types " + moveableEntity.GetType() + " " + otherEntity.GetType()); 
                        }
                    }
                }

                
            }
        }

        static bool collision(Circle one, Circle two)
        {
            float distance = Vector2.Distance(one.origin, two.origin);

            if (distance < one.radius + two.radius)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        
    }

   

    public class CollisionArgs : EventArgs
    {
        public IEntityPhysics collidingObject { private set; get; }
        public float hitObjectWeight { private set; get; }
        public Vector2 hitObjectVelocity { private set; get; }
        public float hitObjectAngle { private set; get; }

        public CollisionArgs(IEntityPhysics collidingObject, float hitObjectWeight, Vector2 hitObjectVelocity, float hitObjectAngle )
        {
            this.collidingObject = collidingObject;
            this.hitObjectWeight = hitObjectWeight;
            this.hitObjectVelocity = hitObjectVelocity;
            this.hitObjectAngle = hitObjectAngle;
        }
    }
}
