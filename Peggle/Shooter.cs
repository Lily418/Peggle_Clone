using System;
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
        float aimingAngle = 0;
        Color color;
        Rectangle basePosition;
        IShooterController shooterController;

        public Shooter(Game game, Color color, Rectangle basePosition, IShooterController controller) : base(game)
        {
            this.basePosition = basePosition;
            this.color = color;
            this.shooterController = controller;
        }

        public override void Update(GameTime gameTime)
        {
            ShooterInstructions nextInstruction = shooterController.getShooterInstructions(gameTime.TotalGameTime);
            aimingAngle += nextInstruction.movementDirection;

            aimingAngle = MathHelper.Clamp(aimingAngle, -1.2f, 1.2f);


        }

        public override void Draw(GameTime gameTime)
        {
            DrawHelper draw = DrawHelper.getInstance();

            SpriteBatch sb = draw.sb;
            sb.Begin();

            sb.Draw(draw.dummyTexture, basePosition, color);

            int pipeWidth = basePosition.Width / 4;
            int pipeHeight = basePosition.Height * 4;
            Rectangle pipePosition = new Rectangle(basePosition.Center.X, basePosition.Y, pipeWidth, pipeHeight);
            sb.Draw(draw.dummyTexture, pipePosition, null, color, aimingAngle, new Vector2(0.5f, 0), SpriteEffects.None, 0);



            sb.End();

            
            
        }


    }
}
