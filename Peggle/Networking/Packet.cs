using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Networking
{
    abstract class Packet
    {
        protected String data;

        public String getPacket()
        {
            return getCheckSum() + ";" + getType() + ";" + data;
        }

        protected abstract String getType();

        private String getCheckSum()
        {
            return "0";
        }

    }
}
