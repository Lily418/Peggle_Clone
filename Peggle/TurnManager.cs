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

        public TurnManager(Game game, Queue<Shooter> shooters) : base (game)
        {
            EventHandlers.ballFallen += ballFallenEventHandler;

            shooterQueue = shooters;

            Game1 game1 = (Game1)game;
            foreach (Shooter shooter in shooters)
            {
                game1.addGameComponent(shooter);
            }

            try
            {
                activeShooter = shooterQueue.Dequeue();
            }
            catch (InvalidOperationException e)
            {
                Debug.Assert(false, "Queue passed to ShooterController should not be empty");
                throw e;
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
