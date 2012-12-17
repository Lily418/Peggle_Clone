using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Networking
{
    class UDPConnection
    {
        const int DEFAULT_PORT = 11000;

        UdpClient udpClient = new UdpClient(DEFAULT_PORT);
        IPEndPoint remoteIpEndPoint;

        public UDPConnection(String address)
        {
            remoteIpEndPoint = new IPEndPoint(IPAddress.Parse(address), DEFAULT_PORT);
            udpClient.Connect(address, DEFAULT_PORT);
        }

        public void send(String data)
        {
            Byte[] sendBytes = Encoding.ASCII.GetBytes(data);
            udpClient.Send(sendBytes, sendBytes.Length);
        }

        public String receive()
        {
            Byte[] receiveBytes = udpClient.Receive(ref remoteIpEndPoint);
            return Encoding.ASCII.GetString(receiveBytes);
        }
    }
}
