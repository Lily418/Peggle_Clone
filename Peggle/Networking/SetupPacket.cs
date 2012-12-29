using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Peggle;

namespace Networking
{
    class SetupPacket : Packet
    {
        public SetupPacket(List<Shooter> shooters, Shooter clientsShooter)
        {
            foreach (Shooter shooter in shooters)
            {
                data += shooter.identifier + ",";
            }

            data = data.TrimEnd(',');
            data += "#" + clientsShooter.identifier;
        }

        protected override string getType()
        {
            return "SetupPacket";
        }
    }
}
