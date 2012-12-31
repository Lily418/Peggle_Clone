using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Peggle
{
    class PlayerRequestRecord
    {
        public IPAddress ip;
        public DateTime firstSent;
        public DateTime? lastResent;

        public PlayerRequestRecord(IPAddress ip, DateTime firstSent)
        {
            this.ip = ip;
            this.firstSent = firstSent;
            this.lastResent = firstSent;
        }

    }
}
