using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using Helper;

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

        public static void shutdown()
        {
            udpClient.Close();
            reciveThread.Abort();
        }

        public static void receive()
        {
            while(true)
            {
                try
                {
                    IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, DEFAULT_PORT);
                    Byte[] receiveBytes = udpClient.Receive(ref remoteIpEndPoint);
                    String[] packet = Encoding.ASCII.GetString(receiveBytes).Split(';');

                    if (checkChecksum(packet))
                    {
                        switch (packet[1])
                        {
                            case "PlayerRequest":
                                PacketEvents.raiseEvent(new PlayerRequestArgs(remoteIpEndPoint.Address, packet[2]));
                                break;

                            case "PlayerRequestResponse":
                                PacketEvents.raiseEvent(new PlayerRequestResponseArgs(remoteIpEndPoint.Address, Boolean.Parse(packet[2])));
                                break;

                            case "TargetAnglePacket":
                                String[] targetPacketSplit = packet[2].Split(',');
                                Debug.WriteLine(packet[2]);
                                PacketEvents.raiseEvent(new TargetAngleArgs(Convert.ToUInt32(targetPacketSplit[0]), Convert.ToSingle(targetPacketSplit[1])));
                                break;

                            case "SetupPacket":
                                String[] dataSplit = packet[2].Split('#');
                                List<uint> identfiers = new List<uint>();
                                String[] identfierStrings = dataSplit[0].Split(',');

                                foreach (String intString in identfierStrings)
                                {
                                    identfiers.Add(Convert.ToUInt32(intString));
                                }

                                uint clientIdentifer = Convert.ToUInt32(dataSplit[1]);

                                PacketEvents.raiseEvent(new SetupArgs(identfiers, clientIdentifer));
                                break;

                        }
                    }
                }
                catch (SocketException)
                {

                }
                

            }
        }

        private static bool checkChecksum(String[] packet)
        {
            return true;
        }
    }

}
