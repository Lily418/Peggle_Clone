﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using Helper;

#if DEBUG
using Microsoft.Xna.Framework.Input;
#endif

namespace Peggle
{
    public class Shooter : DrawableGameComponent
    {
        const int PIPE_HEIGHT_MULTIPLER = 4;
        const int PIPE_WIDTH_DIVISOR = 4;
        public const float MIN_ROTATION = MathHelper.PiOver2 - (MathHelper.Pi / 3);
        public const float MAX_ROTATION = MathHelper.PiOver2 + (MathHelper.Pi / 3);

        public float aimingAngle { private set; get; } 
        Color color;
        Rectangle basePosition;
        IShooterController shooterController;
        Ball ball = null;

        List<Target> targets = new List<Target>();

        public Shooter(Color color, Rectangle basePosition, IShooterController controller) : base(Game1.game)
        {
            aimingAngle = MathHelper.PiOver2;
            this.basePosition = basePosition;
            this.color = color;
            this.shooterController = controller;
            EventHandlers.ballFallen += ballFallenEventHandler;
            EventHandlers.turnChange += turnChangeEventHandler;
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
                    ball = new Ball(this, calculateBallStartingLocation(aimingAngle), aimingAngle);
                    Game1.addGameComponent(ball);
                }
            }


        }

        public Circle calculateBallStartingLocation(float firingAngle)
        {
            const int BALL_RADIUS = 7;
            Vector2 startingVector = new Vector2(basePosition.Center.X, basePosition.Center.Y) + new PolarCoordinate(basePosition.Height * PIPE_HEIGHT_MULTIPLER, firingAngle).toCartesian();
            return new Circle(startingVector, BALL_RADIUS);
        }

        public override void Draw(GameTime gameTime)
        {            
            DrawHelper draw = DrawHelper.getInstance();

            SpriteBatch sb = draw.sb;
            sb.Begin();

            sb.Draw(draw.dummyTexture, basePosition, color);

            int pipeWidth  = basePosition.Width / PIPE_WIDTH_DIVISOR;
            int pipeHeight = basePosition.Height * PIPE_HEIGHT_MULTIPLER;
            Rectangle pipePosition = new Rectangle(basePosition.Center.X, basePosition.Y, pipeHeight, pipeWidth);
            sb.Draw(draw.dummyTexture, pipePosition, null, color, aimingAngle, new Vector2(0f, 0.5f), SpriteEffects.None, 0);

            sb.End();
        }

        public void ballFallenEventHandler(object sender, BallFallenArgs e)
        {
            Game1.removeGameComponent(ball);
            ball = null;

            for(int i = 0; i < targets.Count; i++)
            {
                if (targets[i].hit)
                {
                    Game1.removeGameComponent(targets[i]);
                    targets.Remove(targets[i]);
                    i--;
                }
            }
        }

        public void turnChangeEventHandler(object sender, TurnChangeArgs e)
        {
            if (e.activatedShooter == this)
            {
                foreach (Target target in targets)
                {
                    target.Enabled = true;
                    target.Visible = true;
                }
            }
            else
            {
                Debug.WriteLine("Well that wasn't me " + shooterController.GetType() +" " +targets.Count);
                foreach (Target target in targets)
                {
                    target.Enabled = false;
                    target.Visible = false;
                }
            }
        }


        public void addTarget(Target target)
        {
            target.setColor(color);
            targets.Add(target);
            Game1.addGameComponent(target);
        }


    }
}
