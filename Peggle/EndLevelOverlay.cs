using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Helper;

namespace Peggle
{
    class EndLevelOverlay : DrawableGameComponent
    {
        const String TITLE_TEXT  = "Game Over";
        const String PROMPT_TEXT = "Press Enter To Continue";

        public EndLevelOverlay() : base(Game1.game)
        {
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Enter))
            {
                EventHandlers.raiseEvent(new LevelResetRequestArgs());
            }
        }

        public override void Draw(GameTime gameTime)
        {
            DrawHelper dh = DrawHelper.getInstance();

            dh.sb.Begin();

            Rectangle viewport = Game1.graphics.GraphicsDevice.Viewport.Bounds;
            Rectangle background = new Rectangle((int)(viewport.Width * 0.25f), (int)(viewport.Height * 0.1f), (int)(viewport.Width * 0.5f), (int)(viewport.Width * 0.8f));

            dh.sb.Draw(dh.dummyTexture, background, Color.MidnightBlue);

            float titleY = background.Y + background.Height * 0.1f;
            float titleX = background.X + (background.Width * 0.5f - (dh.font.MeasureString(TITLE_TEXT).X / 2));
            dh.sb.DrawString(dh.font, TITLE_TEXT, new Vector2(titleX, titleY), Color.White);

            float labelX = background.X + background.Width * 0.1f;
            float labelY = background.Y + background.Height * 0.2f;
            foreach(Score score in Game1.getComponents().OfType<Score>())
            {
                dh.sb.DrawString(dh.font, score.label + " " + score.score, new Vector2(labelX, labelY), Color.White);
                labelY += dh.font.MeasureString(score.label + " " + score.score).Y + 5f;
            }

            float promptY = background.Y + background.Height * 0.9f;
            float promptX = background.X + (background.Width / 2 - (dh.font.MeasureString(PROMPT_TEXT).X / 2));

            dh.sb.DrawString(dh.font, PROMPT_TEXT, new Vector2(promptX, promptY), Color.White);

            dh.sb.End();
        }
    }
}
