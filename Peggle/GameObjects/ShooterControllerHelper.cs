using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Peggle
{
    static class ShooterControllerHelper
    {
        const float MOVEMENT_SPEED = 0.02f;

        public static ShooterInstructions towardsTargetAngle(float targetPosition, float aimingAngle)
        {
            float difference = targetPosition - aimingAngle;

            if (Math.Abs(difference) < MOVEMENT_SPEED)
            {
                return new ShooterInstructions(difference, false);
            }
            else
            {
                if (difference > 0)
                {
                    return new ShooterInstructions(MOVEMENT_SPEED, false);
                }
                else
                {
                    return new ShooterInstructions(-MOVEMENT_SPEED, false);
                }
            }
        }
    }
}
