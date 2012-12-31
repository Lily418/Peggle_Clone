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

        static Dictionary<IPAddress, Packet> lastSent = new Dictionary<IPAddress, Packet>();

        public static void send(Packet packet, IPAddress address)
        {
            lastSent[address] = packet;
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

                    Console.WriteLine(Encoding.ASCII.GetString(receiveBytes));

                    Debug.WriteLine(packet[1]);

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

                            case "ShutDownPacket":
                                PacketEvents.raiseEvent(new ClientShutdownArgs(remoteIpEndPoint.Address));
                                break;

                            case "ResendRequestPacket":

                                if (lastSent.Keys.Contains(remoteIpEndPoint.Address))
                                {
                                    send(lastSent[remoteIpEndPoint.Address], remoteIpEndPoint.Address);
                                }
                                break;

                        }
                    }
                }
                catch (Exception e)
                {
                    //Exceptions can be caused on shutdown such as SocketException and Object Disposed.
                    //I want to hide these exceptions from the user but other exceptions not caused on shutdown should be seen by debugger
                    if (!(e is ThreadAbortException))
                    {
                        Debug.Assert(false, e.Message);
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
