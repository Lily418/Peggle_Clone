using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Peggle.Networking
{
    class PlayerRequestResponse : Packet
    {
        public PlayerRequestResponse(bool accept)
        {
            data = accept.ToString();
        }

        protected override String getType()
        {
            return "PlayerRequestResponse";
        }
    }
}
