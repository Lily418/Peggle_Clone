using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Peggle
{
    class ShootSimulator
    {
        public int actionValue { get; private set; }
        Ball simulatedBall;
        List<Target> targetsHit = new List<Target>();



        public ShootSimulator(GameTime time, Shooter shooter, float aimAngle)
        {
            actionValue = 0;
            EventHandlers.getInstance().collision += collisionEventHandler;
            simulatedBall = new Ball(null, shooter.calculateBallStartingLocation(aimAngle), aimAngle, true);

            Game1.addGameComponent(simulatedBall);

            while (!simulatedBall.ballFallen())
            {
                Game1.levelStateManager.currentLevel.physicsProcessor.Update(time);
            }

            EventHandlers.getInstance().collision -= collisionEventHandler;
            Game1.removeGameComponent(simulatedBall);

        }

        public void collisionEventHandler(object sender, CollisionArgs e)
        {
            if (e.collidingObject is Ball && e.collidingObject.Equals(simulatedBall))
            {
                if (e.hitObject != null && e.hitObject is Target)
                {
                    Target targetHit = (Target)e.hitObject;

                    if (!targetsHit.Contains(targetHit))
                    {
                        actionValue += Target.POINTS;

                        targetsHit.Add(targetHit);
                    }
                }
            }
        }
    }
}
