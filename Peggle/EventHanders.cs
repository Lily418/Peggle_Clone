using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Peggle
{
    static class EventHanders
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
}
