using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Peggle
{
    class Shooter : DrawableGameComponent
    {
        float aimingAngle = 0;
        Color color;
        Rectangle basePosition;

        public Shooter(Game game, Color color, Rectangle basePosition) : base(game)
        {
            this.basePosition = basePosition;
            this.color = color;
        }

        public override void Update(GameTime gameTime)
        {
            aimingAngle -= 0.02f;
        }

        public override void Draw(GameTime gameTime)
        {
            DrawHelper draw = DrawHelper.getInstance();

            SpriteBatch sb = draw.sb;
            sb.Begin();

            sb.Draw(draw.dummyTexture, basePosition, color);

            int pipeWidth = basePosition.Width / 4;
            int pipeHeight = basePosition.Height * 4;
            Rectangle pipePosition = new Rectangle(basePosition.Center.X - (pipeWidth/2), basePosition.Y, pipeWidth, pipeHeight);
            sb.Draw(draw.dummyTexture, pipePosition, null, color, aimingAngle, new Vector2(0, 0), SpriteEffects.None, 0);

            sb.End();

            
            
        }


    }
}
