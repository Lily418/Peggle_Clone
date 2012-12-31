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
        public static EventHandler<TargetAngleArgs> targetAngle;
        public static EventHandler<SetupArgs> setup;
        public static EventHandler<ClientShutdownArgs> clientShutdown;
        
        public static void raiseEvent(EventArgs e)
        {
            if (e is PlayerRequestArgs)
            {
                if (playerRequest != null)
                {
                    playerRequest("PacketEvents", (PlayerRequestArgs)e);
                }
            }
            else if (e is PlayerRequestResponseArgs)
            {
                if (playerRequestResponse != null)
                {
                    playerRequestResponse("PacketEvents", (PlayerRequestResponseArgs)e);
                }
            }
            else if (e is TargetAngleArgs)
            {
                if (targetAngle != null)
                {
                    targetAngle("PacketEvents", (TargetAngleArgs)e);
                }
            }
            else if (e is SetupArgs)
            {
                if (setup != null)
                {
                    setup("PacketEvents", (SetupArgs)e);
                }
            }
            else if (e is ClientShutdownArgs)
            {
                if (clientShutdown != null)
                {
                    clientShutdown("PacketEvents", (ClientShutdownArgs)e);
                }
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

    public class TargetAngleArgs : EventArgs
    {
        public uint identifer;
        public float angle;

        public TargetAngleArgs(uint identifer, float angle)
        {
            this.identifer = identifer;
            this.angle = angle;
        }
    }

    public class SetupArgs : EventArgs
    {
        public List<uint> shooterIdentfiers;
        public uint clientIdentfier;

        public SetupArgs(List<uint> shooterIdentfiers, uint clientIdentfier)
        {
            this.shooterIdentfiers = shooterIdentfiers;
            this.clientIdentfier = clientIdentfier;
        }
    }

    public class ClientShutdownArgs : EventArgs
    {
        public IPAddress address;

        public ClientShutdownArgs(IPAddress address)
        {
            this.address = address;
        }
    }
}
