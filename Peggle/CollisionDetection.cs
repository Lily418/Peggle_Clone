using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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
                    EventHanders.raiseEvent(new CollisionArgs(moveableEntity, float.PositiveInfinity, Vector2.Zero, 0), EventType.Collision);
                }
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
