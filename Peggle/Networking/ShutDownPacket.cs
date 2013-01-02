using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Peggle.Networking
{
    class ShutDownPacket : Packet
    {
        protected override string getType()
        {
            return "ShutDownPacket";
        }
    }
}
