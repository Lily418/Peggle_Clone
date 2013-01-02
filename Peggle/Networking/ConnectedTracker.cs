using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Peggle.Networking
{
    static class ConnectedTracker
    {
        static List<IPAddress> connectedClients = new List<IPAddress>();

        public static void addClient(IPAddress address)
        {
            connectedClients.Add(address);
        }

        public static void removeClient(IPAddress address)
        {
            connectedClients.Remove(address);
        }

        public static void inform()
        {
            //Girlfriends cat ran across the keyboard and added the following comment
            ///////////////////////////*-///////////
            //I feel that it might be important so havn't removed it.

            foreach (IPAddress address in connectedClients)
            {
                NetworkInterface.send(new ShutDownPacket(), address);
            }
        }

        public static bool contains(IPAddress address)
        {
            return connectedClients.Contains(address);
        }
    }
}
