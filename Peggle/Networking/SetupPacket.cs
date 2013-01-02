using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Peggle;
using Microsoft.Xna.Framework;

namespace Peggle.Networking
{
    class SetupPacket : Packet
    {
        public SetupPacket(List<Shooter> shooters, Shooter clientsShooter)
        {
            foreach (Shooter shooter in shooters)
            {
                data += shooter.identifier + "@" + colorToString(shooter.color) + ",";
            }

            data = data.TrimEnd(',');
            data += "#" + clientsShooter.identifier;
        }

        private String colorToString(Color color)
        {
            return color.R + "^" + color.G + "^" + color.B;
        }

        protected override string getType()
        {
            return "SetupPacket";
        }
    }
}
