using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Helper;
using System.Diagnostics;

namespace Peggle.Networking
{
    class TimeoutInterface : DrawableGameComponent
    {
        SetupMenu parent;
        PlayerRequestRecord  request;

        public TimeoutInterface(SetupMenu parent, PlayerRequestRecord request)
            : base(Game1.game)
        {
            this.parent = parent;
            this.request = request;
            Dialog.gainControl(this);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardInput.KeyboardButtons keyboardButtons = KeyboardInput.getInstance().buttonStates;

            if (parent.playerRequests.Contains(request))
            {
                if (keyboardButtons.keyPresses[Keys.Enter] == KeyboardInput.KeyboardActions.Pressed)
                {
                    parent.playerRequests.TryTake(out request);
                    Game1.removeGameComponent(this);
                    Dialog.returnControl(this);
                }
                else if (keyboardButtons.keyPresses[Keys.Escape] == KeyboardInput.KeyboardActions.Pressed)
                {
                    System.Net.IPAddress ip = request.ip;
                    parent.playerRequests.TryTake(out request);
                    parent.playerRequests.Add(new PlayerRequestRecord(ip, DateTime.Now));
                    Game1.removeGameComponent(this);
                    Dialog.returnControl(this);
                }
            }
            else
            {
                Game1.removeGameComponent(this);
                Dialog.returnControl(this);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            DrawHelper dh = DrawHelper.getInstance();

            dh.sb.Begin();

            dh.sb.DrawString(dh.font, "No reponse from "  + request.ip, new Vector2(10, 10), Color.White);
            dh.sb.DrawString(dh.font, "Enter - Start Game without " + request.ip,      new Vector2(10, 30), Color.White);
            dh.sb.DrawString(dh.font, "Esc   - Retry ", new Vector2(10, 45), Color.White);
            dh.sb.End();
        }
    }
}
