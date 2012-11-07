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
        //public static EventHandler<PlayerInputArgs> playerInput;

        public static void raiseEvent(EventArgs e, EventType type)
        {
            try
            {
                switch (type)
                {
                    //case EventType.PlayerInput:

                        //if (playerInput != null)
                        //{
                            //playerInput("EventHandler", (PlayerInputArgs)e);
                        //}

                        //break;
                }
            }
            catch (InvalidCastException)
            {
                Debug.WriteLine("Event Not Raised, Event: " + e.GetType() +" is not type " + type);
            }
        }

        public enum EventType
        {
            PlayerInput
        }
    }
}
