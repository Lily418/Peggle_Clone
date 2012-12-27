using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Networking
{
    class PacketEvents
    {
        public static EventHandler<PlayerRequestArgs> playerRequest;
        public static EventHandler<PlayerRequestResponseArgs> playerRequestResponse;
        
        public static void raiseEvent(EventArgs e)
        {
            if (e is PlayerRequestArgs && playerRequest != null)
            {
                playerRequest("PacketEvents", (PlayerRequestArgs)e);
            }
            else if (e is PlayerRequestResponseArgs && playerRequestResponse != null)
            {
                playerRequestResponse("PacketEvents", (PlayerRequestResponseArgs)e);
            }
            else
            {
                throw new Exception("Unknown Event Type");
            }
        }
    }


    public class PlayerRequestArgs : EventArgs
    {
        public IPAddress ip;
        public String machineName;

        public PlayerRequestArgs(IPAddress ip, String machineName)
        {
            this.ip = ip;
            this.machineName = machineName;
        }
    }

    public class PlayerRequestResponseArgs : EventArgs
    {
        public IPAddress ip;
        public bool answer;

        public PlayerRequestResponseArgs(IPAddress ip, bool answer)
        {
            this.ip = ip;
            this.answer = answer;
        }
    }
}
