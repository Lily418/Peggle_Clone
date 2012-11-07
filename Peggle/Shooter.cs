﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace Peggle
{
    class Shooter : DrawableGameComponent
    {
        const int PIPE_HEIGHT_MULTIPLER = 4;
        const int PIPE_WIDTH_DIVISOR = 4;

        Game1 game;
        float aimingAngle = 0;
        Color color;
        Rectangle basePosition;
        IShooterController shooterController;
        Ball ball = null;

        public Shooter(Game game, Color color, Rectangle basePosition, IShooterController controller) : base(game)
        {
            this.basePosition = basePosition;
            this.color = color;
            this.shooterController = controller;
            this.game = (Game1)game;
        }

        public override void Update(GameTime gameTime)
        {
            ShooterInstructions nextInstruction = shooterController.getShooterInstructions(gameTime.TotalGameTime);
            aimingAngle += nextInstruction.movementDirection;

            aimingAngle = MathHelper.Clamp(aimingAngle, -1.2f, 1.2f);


            

            if (ball == null)
            {
                if (nextInstruction.fireBall)
                {
                    float angle = aimingAngle;


                    Debug.WriteLine(angle );
                    ball = new Ball((Game)game, calculateBallStartingLocation(), angle);
                    game.addGameComponent(ball);
                }
            }

            //DEBUG CODE
            else if (!Game1.graphics.GraphicsDevice.Viewport.Bounds.Contains(ball.location.topLeft.toPoint()))
            {
                game.removeGameComponent(ball);
                ball = null;
            }
            //END DEBUG


        }

        private Location calculateBallStartingLocation()
        {
            const int BALL_DIAMETER = 10;
            Vector2 startingVector = new Vector2(basePosition.Center.X, basePosition.Top) + new PolarCoordinate(basePosition.Height * PIPE_HEIGHT_MULTIPLER, aimingAngle).toCartesian();
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
            Rectangle pipePosition = new Rectangle(basePosition.Center.X, basePosition.Y, pipeWidth, pipeHeight);
            sb.Draw(draw.dummyTexture, pipePosition, null, color, aimingAngle, new Vector2(0.5f, 0), SpriteEffects.None, 0);



            sb.End();

            
            
        }


    }
}
