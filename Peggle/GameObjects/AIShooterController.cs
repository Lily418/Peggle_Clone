﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Helper;

namespace Peggle
{
    class AI : IShooterController
    {
        const int NO_SHOTS_SIMULATED = 60;
        const float MOVEMENT_SPEED = 0.02f;

        float? targetPosition = null;

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
                return ShooterControllerHelper.towardsTargetAngle((float)targetPosition, shooter.aimingAngle);
            }
            
        }

        
        private float calculateTargetAngle(GameTime currentElapsedTime, Shooter shooter)
        {
            PriorityQueue<int, float> possibleShots = new PriorityQueue<int, float>(Comparer<int>.Default);

            const float interval = (Shooter.MAX_ROTATION - Shooter.MIN_ROTATION) / NO_SHOTS_SIMULATED;

            for (float angle = Shooter.MIN_ROTATION; angle < Shooter.MAX_ROTATION; angle += interval)
            {
                possibleShots.enqueue(new KeyValuePair<int, float>(new ShootSimulator(currentElapsedTime, shooter, angle).actionValue, angle));
            }

            List<KeyValuePair<int, float>> bestShots = possibleShots.getBaseStorage().FindAll(shot => shot.Key == possibleShots.last().Key);

            return bestShots[RandomHelper.getRandom().Next(bestShots.Count)].Value;
        }

    }
}
