using System;
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
        List<Target> targetsHit = new List<Target>();



        public ShootSimulator(Game game, GameTime time, Shooter shooter, float aimAngle)
        {
            actionValue = 0;
            EventHandlers.collision += collisionEventHandler;
            simulatedBall = new Ball(Game1.game, shooter.calculateBallStartingLocation(aimAngle), aimAngle, true);

            Game1.addGameComponent(simulatedBall);


            while (!simulatedBall.ballFallen())
            {
                Game1.currentLevel.physicsProcessor.Update(time);
            }


            EventHandlers.collision -= collisionEventHandler;
            Game1.removeGameComponent(simulatedBall);

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
