﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using System.Threading;

namespace Peggle
{
    class AI : IShooterController
    {
        //This number was choosen to make the amount of checks equal to the amount of positions the player can move into
        const int NO_SHOTS_SIMULATED = 80;

        float? targetPosition = null;
        const float MOVEMENT_SPEED = 0.02f;

        readonly TimeSpan AiWait = TimeSpan.FromSeconds(0.5);
        TimeSpan currentWait = TimeSpan.Zero;

        public ShooterInstructions getShooterInstructions(GameTime gameTime, Shooter shooter)
        {
            if (targetPosition == null)
            {
                targetPosition = calculateTargetAngle(gameTime, shooter);
                currentWait = AiWait;
                Debug.Assert(targetPosition <= Shooter.MAX_ROTATION && targetPosition >= Shooter.MIN_ROTATION, "Target Angle must be between the clamp values impossed by the shooter class");
            }

            if (shooter.aimingAngle == targetPosition)
            {
                if (currentWait > TimeSpan.Zero)
                {
                    currentWait -= gameTime.ElapsedGameTime;
                    return new ShooterInstructions(0.0f, false);
                }
                else
                {
                    targetPosition = null;
                    return new ShooterInstructions(0.0f, true);
                }
            }
            else
            {
                float difference = (float)targetPosition - shooter.aimingAngle;

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

        
        private float calculateTargetAngle(GameTime currentElapsedTime, Shooter shooter)
        {
            PriorityQueue<int, float> possibleShots = new PriorityQueue<int, float>(Comparer<int>.Default);

            const float interval = (Shooter.MAX_ROTATION - Shooter.MIN_ROTATION) / NO_SHOTS_SIMULATED;

            TimeSpan max = TimeSpan.Zero;
            float maxAngle = 0f;
            for (float angle = Shooter.MIN_ROTATION; angle < Shooter.MAX_ROTATION; angle += interval)
            {
                Debug.WriteLine(angle);

                DateTime inital = DateTime.Now;
                possibleShots.enqueue(new KeyValuePair<int, float>(new ShootSimulator(currentElapsedTime, shooter, angle).actionValue, angle));
                TimeSpan difference = DateTime.Now - inital;

                if (difference > max)
                {
                    maxAngle = angle;
                    max = difference;
                }

                
            }

            Debug.WriteLine(maxAngle);

            return possibleShots.last().Value;
        }

    }
}
