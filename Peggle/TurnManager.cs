﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

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
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException("Queue passed to ShooterController should not be empty", e);
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
