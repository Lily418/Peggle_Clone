using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Peggle
{
    class TurnManager : GameComponent
    {
        Shooter activeShooter;
        Queue<Shooter> shooterQueue = new Queue<Shooter>(2);

        public TurnManager(Queue<Shooter> shooters) : base (Game1.game)
        {
            EventHandlers.ballFallen += ballFallenEventHandler;

            shooterQueue = shooters;

            foreach (Shooter shooter in shooters)
            {
                Game1.addGameComponent(shooter);
            }

            try
            {
                activeShooter = shooterQueue.Dequeue();
            }
            catch (InvalidOperationException)
            {
                Debug.Assert(false, "Queue passed to ShooterController should not be empty");
                throw;
            }
        }

        public override void Update(GameTime gameTime)
        {
            activeShooter.processInput(gameTime);
        }

        public void ballFallenEventHandler(object sender, BallFallenArgs e)
        {
            //Check ball is not a ball being simulated by the AI
            if(!e.ball.isSimulation)
            {
            shooterQueue.Enqueue(activeShooter);
            activeShooter = shooterQueue.Dequeue();
            }
        }
    }
}
