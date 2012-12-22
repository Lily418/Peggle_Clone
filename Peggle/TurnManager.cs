using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Peggle
{
    public class TurnManager : GameComponent
    {
        Shooter activeShooter;
        Queue<Shooter> shooterQueue = new Queue<Shooter>(2);
        public int turnCount { private set; get; }

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
                EventHandlers.raiseEvent(new TurnChangeArgs(activeShooter, null));
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException("Queue passed to ShooterController should not be empty", e);
            }

            turnCount = 0;
        }

        public override void Update(GameTime gameTime)
        {
            if (activeShooter.processInput(gameTime))
            {
                turnCount++;
            }
        }

        public void ballFallenEventHandler(object sender, BallFallenArgs e)
        {
            //Check ball is not a ball being simulated by the AI
            if(!e.ball.isSimulation)
            {
            Shooter deactivatedShooter = activeShooter;
            shooterQueue.Enqueue(activeShooter);
            activeShooter = shooterQueue.Dequeue();

            EventHandlers.raiseEvent(new TurnChangeArgs(activeShooter, deactivatedShooter));
            }
        }

        public int noOfPlayers()
        {
            return shooterQueue.Count + 1;
        }
    }
}
