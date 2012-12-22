using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Networking
{
    static class NetworkInterface
    {
        static internal ConcurrentQueue<String> receivedPackets = new ConcurrentQueue<String>();

        const int DEFAULT_PORT = 10823;
        static UdpClient udpClient = new UdpClient(DEFAULT_PORT);
        static IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, DEFAULT_PORT);

        static Thread reciveThread = new Thread(new ThreadStart(receive));

        public static void send(String data, IPEndPoint endPoint)
        {
            Byte[] sendBytes = Encoding.ASCII.GetBytes(data);
            udpClient.Send(sendBytes, sendBytes.Length, endPoint);
        }

        public static void startRecivingPackets()
        {
            reciveThread.Start();
        }

        public static void stopRecivingPackets()
        {
            reciveThread.Abort();
        }

        public static void receive()
        {
            while(true)
            {
                Byte[] receiveBytes = udpClient.Receive(ref remoteIpEndPoint);
                receivedPackets.Enqueue(Encoding.ASCII.GetString(receiveBytes));
            }
        }
    }
}
