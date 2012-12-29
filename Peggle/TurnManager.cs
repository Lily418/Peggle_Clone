using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Helper;

namespace Peggle
{
    public class TurnManager : DrawableGameComponent
    {
        Shooter activeShooter;
        Queue<Shooter> shooterQueue = new Queue<Shooter>();
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

        public override void Draw(GameTime gameTime)
        {
            DrawHelper dh = DrawHelper.getInstance();

            dh.sb.Begin();

            dh.sb.DrawString(dh.font, "Current Player: " + activeShooter.shooterName, new Vector2(10, 570), Color.White);

            dh.sb.End();
        }
    }
}
