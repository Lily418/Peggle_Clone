using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Networking
{
    class PlayerRequest : Packet
    {
        public PlayerRequest()
        {
            data = Environment.MachineName;
        }

        protected override String getType()
        {
            return "PlayerRequest";
        }
    }
}
