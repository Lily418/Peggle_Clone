using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Networking;

namespace Networking
{
    public class TargetAnglePacket : Packet
    {
        public TargetAnglePacket(uint identifier, float targetAngle)
        {
            data = identifier +","+ targetAngle.ToString();
        }

        protected override String getType()
        {
            return "TargetAnglePacket";
        }
    }
}
