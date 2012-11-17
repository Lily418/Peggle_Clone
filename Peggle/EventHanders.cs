using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Peggle
{
    static class EventHandlers
    {
        public static EventHandler<CollisionArgs> collision;
        public static EventHandler<BallFallenArgs> ballFallen;

        public static void raiseEvent(EventArgs e, EventType type)
        {
            try
            {
                switch (type)
                {
                    case EventType.Collision:

                        if (collision != null)
                        {
                            collision("EventHandler", (CollisionArgs)e);
                        }

                        break;
                    case EventType.BallFallen:
                        if (ballFallen != null)
                        {
                            ballFallen("EventHandler", (BallFallenArgs)e);
                        }

                        break;
                }
            }
            catch (InvalidCastException)
            {
                Debug.Assert(false, "Event Not Raised, Event: " + e.GetType() +" is not type " + type);
            }
        }


    }

    public enum EventType
    {
        Collision, BallFallen
    }

    class BallFallenArgs : EventArgs
    {
        public Ball ball { private set; get; }

        public BallFallenArgs(Ball ball)
        {
            this.ball = ball;
        }

    }

    public class CollisionArgs : EventArgs
    {
        public IEntityPhysics collidingObject { private set; get; }
        //These variables are kept seperatly as they are not part of Entity interface, they are 'Made Up' by the collision detection system to deal with walls
        public Vector2 hitObjectVelocity { private set; get; }
        public float hitObjectAngle { private set; get; }
        public Entity hitObject { private set; get; }
        public float penetration { private set; get; }

        public CollisionArgs(IEntityPhysics collidingObject, Entity hitObject, Vector2 hitObjectVelocity, float hitObjectAngle, float penetration)
        {
            this.hitObject = hitObject;
            this.collidingObject = collidingObject;
            this.hitObjectVelocity = hitObjectVelocity;
            this.hitObjectAngle = hitObjectAngle;
            this.penetration = penetration;
        }
    }
}
