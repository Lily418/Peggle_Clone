﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
using Helper;

#if DEBUG
using Microsoft.Xna.Framework.Input;
#endif

namespace Peggle
{
    class Shooter : DrawableGameComponent
    {
        const int PIPE_HEIGHT_MULTIPLER = 4;
        const int PIPE_WIDTH_DIVISOR = 4;
        public const float MIN_ROTATION = MathHelper.PiOver2 - (MathHelper.Pi / 3);
        public const float MAX_ROTATION = MathHelper.PiOver2 + (MathHelper.Pi / 3);

        Game1 game;
        public float aimingAngle { private set; get; } 
        Color color;
        Rectangle basePosition;
        IShooterController shooterController;
        Ball ball = null;

        public Shooter(Game game, Color color, Rectangle basePosition, IShooterController controller) : base(game)
        {
            aimingAngle = MathHelper.PiOver2;
            this.basePosition = basePosition;
            this.color = color;
            this.shooterController = controller;
            this.game = (Game1)game;
            EventHandlers.ballFallen += ballFallenEventHandler;
        }

        public void processInput(GameTime gameTime)
        {
            
#if DEBUG
            KeyboardState currentKeyboardState = Keyboard.GetState();
                if (currentKeyboardState.IsKeyDown(Keys.Q))
                {
                    Debug.WriteLine("Shooter Angle:" + aimingAngle);
                }

#endif

            if (ball == null)
            {
                ShooterInstructions nextInstruction = shooterController.getShooterInstructions(gameTime, this);
                aimingAngle += nextInstruction.movementDirection;
                
                aimingAngle = MathHelper.Clamp(aimingAngle, MIN_ROTATION, MAX_ROTATION);


                if (nextInstruction.fireBall)
                {
                    //aimingAngle = 2.3f;
                    ball = new Ball((Game)game, calculateBallStartingLocation(aimingAngle), aimingAngle);
                    Game1.addGameComponent(ball);
                }
            }


        }

        public Location calculateBallStartingLocation(float firingAngle)
        {
            const int BALL_DIAMETER = 20;
            Vector2 startingVector = new Vector2(basePosition.Center.X, basePosition.Center.Y) + new PolarCoordinate(basePosition.Height * PIPE_HEIGHT_MULTIPLER, firingAngle).toCartesian();
            return new Location(startingVector, BALL_DIAMETER, BALL_DIAMETER);
        }

        public override void Draw(GameTime gameTime)
        {
            
            DrawHelper draw = DrawHelper.getInstance();

            SpriteBatch sb = draw.sb;
            sb.Begin();

            sb.Draw(draw.dummyTexture, basePosition, color);

            int pipeWidth = basePosition.Width / PIPE_WIDTH_DIVISOR;
            int pipeHeight = basePosition.Height * PIPE_HEIGHT_MULTIPLER;
            Rectangle pipePosition = new Rectangle(basePosition.Center.X, basePosition.Y, pipeHeight, pipeWidth);
            sb.Draw(draw.dummyTexture, pipePosition, null, color, aimingAngle, new Vector2(0f, 0.5f), SpriteEffects.None, 0);



            sb.End();

            
            
        }

        public void ballFallenEventHandler(object sender, BallFallenArgs e)
        {
            Game1.removeGameComponent(ball);
            ball = null;
        }


    }
}
