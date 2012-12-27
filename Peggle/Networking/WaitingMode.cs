using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Peggle;
using Helper;

namespace Networking
{
    class WaitingMode : DrawableGameComponent
    {
        public WaitingMode() : base(Game1.game)
        {
            Dialog.gainControl(this);
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime)
        {
            DrawHelper dh = DrawHelper.getInstance();

            dh.sb.Begin();

            dh.sb.DrawString(dh.font, "Waiting for Server to finish setup", new Vector2(10, 10), Color.White);

            dh.sb.End();
        }

    }
}
