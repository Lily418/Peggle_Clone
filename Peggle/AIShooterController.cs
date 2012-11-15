using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace Peggle
{
    class AI : IShooterController
    {
        const int NO_SHOTS_SIMULATED = 100;

        float? targetPosition = null;
        const float MOVEMENT_SPEED = 0.1f;
        Game1 game;

        public AI(Game game)
        {
            this.game = (Game1)game;
        }

        public ShooterInstructions getShooterInstructions(GameTime gameTime, Shooter shooter)
        {
            if (targetPosition == null)
            {
                Debug.WriteLine("Calculating Position" + gameTime.TotalGameTime);
                targetPosition = calculateTargetAngle(gameTime, shooter);

                Debug.Assert(targetPosition <= 1.2f && targetPosition >= -1.2f, "Target Angle must be between the clamp values impossed by the shooter class");
            }

            if (shooter.aimingAngle == targetPosition)
            {
                targetPosition = null;
                return new ShooterInstructions(0.0f, true);
            }
            else
            {
                float difference = (float)targetPosition - shooter.aimingAngle;

                if (difference < MOVEMENT_SPEED)
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

        
        private float calculateTargetAngle(GameTime currentElapsedTime, Shooter shooter)
        {
            PriorityQueue<int, float> possibleShots = new PriorityQueue<int, float>(new IntComparer());

            float interval = (Shooter.ROTATION_LIMIT * 2) / NO_SHOTS_SIMULATED;

            for (float angle = -Shooter.ROTATION_LIMIT; angle < Shooter.ROTATION_LIMIT; angle += interval)
            {
                possibleShots.enqueue(new KeyValuePair<int, float>(new ShootSimulator(game, currentElapsedTime, shooter, angle).actionValue, angle));
            }

            return possibleShots.last();
        }
    }
}
