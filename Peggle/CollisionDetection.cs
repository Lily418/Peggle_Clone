﻿using System;
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
            foreach (IEntityPhysics moveableEntity in Game1.game.getComponents().OfType<IEntityPhysics>())
            {
                if (moveableEntity.location.Left < 0)
                {
                    EventHandlers.raiseEvent(new CollisionArgs(moveableEntity, null, float.PositiveInfinity, Vector2.Zero, MathHelper.Pi, 0), EventType.Collision);
                }
                else if (moveableEntity.location.Right > Game1.graphics.GraphicsDevice.Viewport.Width)
                {
                    EventHandlers.raiseEvent(new CollisionArgs(moveableEntity, null, float.PositiveInfinity, Vector2.Zero, 0, 0), EventType.Collision);
                }

                foreach (Entity otherEntity in Game1.game.getComponents().OfType<Entity>())
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
                                EventHandlers.raiseEvent(new CollisionArgs(moveableEntity, otherEntity, float.PositiveInfinity, Vector2.Zero, hitAngle, penetration), EventType.Collision);
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

        
    }

   

    public class CollisionArgs : EventArgs
    {
        public IEntityPhysics collidingObject { private set; get; }
        //These variables are kept seperatly as they are not part of Entity interface, they are 'Made Up' by the collision detection system
        public float hitObjectWeight { private set; get; }
        public Vector2 hitObjectVelocity { private set; get; }
        public float hitObjectAngle { private set; get; }
        public Entity hitObject { private set; get; }
        public float penetration { private set; get; }

        public CollisionArgs(IEntityPhysics collidingObject, Entity hitObject, float hitObjectWeight, Vector2 hitObjectVelocity, float hitObjectAngle, float penetration)
        {
            this.hitObject = hitObject;
            this.collidingObject = collidingObject;
            this.hitObjectWeight = hitObjectWeight;
            this.hitObjectVelocity = hitObjectVelocity;
            this.hitObjectAngle = hitObjectAngle;
            this.penetration = penetration;
        }
    }
}
