using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Networking
{
    class ResendRequestPacket : Packet
    {
        protected override string getType()
        {
            return "ResendRequestPacket";
        }
    }
}
