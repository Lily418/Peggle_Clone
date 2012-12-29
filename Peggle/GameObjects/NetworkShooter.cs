using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Net;
using Networking;

namespace Peggle
{
    class NetworkShooter : IShooterController
    {
        public IPAddress ipAddress { private set; get; }
        uint identifier;
        float? targetPosition = null;

        public NetworkShooter(IPAddress ipAddress, uint identifier)
        {
            this.ipAddress = ipAddress;
            this.identifier = identifier;
            PacketEvents.targetAngle += targetAngleEventHandler;
        }

        public ShooterInstructions getShooterInstructions(GameTime gameTime, Shooter shooter)
        {
            if (targetPosition != null)
            {
                if (shooter.aimingAngle == targetPosition)
                {
                    targetPosition = null;
                    return new ShooterInstructions(0.0f, true);
                }
                else
                {
                    return ShooterControllerHelper.towardsTargetAngle((float)targetPosition, shooter.aimingAngle);
                }
            }
            else
            {
                return new ShooterInstructions(0.0f, false);
            }
        }

        public void targetAngleEventHandler(object sender, TargetAngleArgs e)
        {
            if (e.identifer == identifier)
            {
                targetPosition = e.angle;
            }
        }
    }
}
