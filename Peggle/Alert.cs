using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Helper;

namespace Peggle
{
    class Alert : DrawableGameComponent
    {
        String text;
        Vector2 position;
        TimeSpan remainingTime;
        Color color;

        public Alert(String text, Vector2 position, TimeSpan time, Color color)
            : base(Game1.game)
        {
            Game1.addGameComponent(this);
            this.text = text;
            this.position = position;
            this.remainingTime = time;
            this.color = color;
        }

        public override void Update(GameTime gameTime)
        {
            remainingTime -= gameTime.ElapsedGameTime;

            if (remainingTime < TimeSpan.Zero)
            {
                Game1.removeGameComponent(this);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            DrawHelper dh = DrawHelper.getInstance();

            dh.sb.Begin();

            dh.sb.DrawString(dh.font, text, position, color);

            dh.sb.End();
        }
    }
}
