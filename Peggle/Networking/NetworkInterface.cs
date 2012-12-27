using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

namespace Networking
{
    static class NetworkInterface
    {
        const int DEFAULT_PORT = 10823;
        static UdpClient udpClient = new UdpClient(DEFAULT_PORT);
        static Thread reciveThread = new Thread(new ThreadStart(receive));

        public static void send(Packet packet, IPAddress address)
        {
            Byte[] sendBytes = Encoding.ASCII.GetBytes(packet.getPacket());
            udpClient.Send(sendBytes, sendBytes.Length, new IPEndPoint(address, DEFAULT_PORT));
        }

        public static void startRecivingPackets()
        {
            reciveThread.Start();
        }

        public static void receive()
        {
            while(true)
            {
                IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, DEFAULT_PORT);
                Byte[] receiveBytes = udpClient.Receive(ref remoteIpEndPoint);
                String[] packet = Encoding.ASCII.GetString(receiveBytes).Split(';');

                if (checkChecksum(packet))
                {
                    switch (packet[1])
                    {
                        case "PlayerRequest": PacketEvents.raiseEvent(new PlayerRequestArgs(remoteIpEndPoint.Address, packet[2])); break;
                        case "PlayerRequestResponse": PacketEvents.raiseEvent(new PlayerRequestResponceArgs(remoteIpEndPoint.Address, Boolean.Parse(packet[2]))); break;
                    }
                }
                

            }
        }

        private static bool checkChecksum(String[] packet)
        {
            return true;
        }
    }

}
