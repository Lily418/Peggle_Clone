﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Peggle
{
    class ShootSimulator
    {
        const int LEVEL_PROGRESS_TARGET_VALUE = 50;
        const int NORMAL_TARGET_VALUE = 10;


        public int actionValue { get; private set; }
        Ball simulatedBall;
        Game1 game;
        List<Target> targetsHit = new List<Target>();



        public ShootSimulator(Game game, GameTime time, Shooter shooter, float aimAngle)
        {
            this.game = (Game1)game;
            actionValue = 0;
            EventHandlers.collision += collisionEventHandler;
            float fireAngle = aimAngle + MathHelper.PiOver2;
            simulatedBall = new Ball(Game1.game, shooter.calculateBallStartingLocation(fireAngle), fireAngle, true);

            this.game.addGameComponent(simulatedBall);


            while (!simulatedBall.ballFallen())
            {
                this.game.currentLevel.physicsProcessor.Update(time);
            }


            EventHandlers.collision -= collisionEventHandler;
            this.game.removeGameComponent(simulatedBall);

        }

        public void collisionEventHandler(object sender, CollisionArgs e)
        {

            
            
            if (e.collidingObject is Ball && e.collidingObject.Equals(simulatedBall))
            {
                if (e.hitObject != null && e.hitObject is Target)
                {
                    Target targetHit = (Target)e.hitObject;

                    if (!targetsHit.Contains(targetHit) && !targetHit.hit)
                    {

                        if (targetHit.countsTowardsLevelProgress)
                        {
                            actionValue += LEVEL_PROGRESS_TARGET_VALUE;
                        }
                        else
                        {
                            actionValue += NORMAL_TARGET_VALUE;
                        }

                        targetsHit.Add(targetHit);
                    }
                }
            }
        }
    }
}
