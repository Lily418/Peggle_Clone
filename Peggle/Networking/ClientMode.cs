using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Helper;
using Peggle;
using System.Net;
using System.Diagnostics;

namespace Peggle.Networking
{
    class ClientMode : DrawableGameComponent
    {
        List<KeyValuePair<IPAddress, String>> requests = new List<KeyValuePair<IPAddress, String>>();

        public ClientMode()
            : base(Game1.game)
        {
            PacketEvents.playerRequest += playerRequestEventHandler;
            Dialog.gainControl(this);
        }

        public override void Draw(GameTime gameTime)
        {
            DrawHelper dh = DrawHelper.getInstance();

            dh.sb.Begin();

            if (requests.Count == 0)
            {
                dh.sb.DrawString(dh.font, "Waiting for Connection", new Vector2(10, 10), Color.White);
                dh.sb.DrawString(dh.font, "Cancel - Esc", new Vector2(10, 30), Color.White);
            }
            else
            {
                dh.sb.DrawString(dh.font, "Connection From " + requests[0].Value, new Vector2(10, 10), Color.White);
                dh.sb.DrawString(dh.font, "Accept - Enter  ",                     new Vector2(10, 40), Color.White);
                dh.sb.DrawString(dh.font, "Reject - Esc    ", new Vector2(10, 60), Color.White);
            }

            dh.sb.End();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardInput.KeyboardButtons keyboardButtons = KeyboardInput.getInstance().buttonStates;
            if (requests.Count != 0)
            {
               
                if (keyboardButtons.keyPresses[Keys.Enter] == KeyboardInput.KeyboardActions.Pressed)
                {
                    ConnectedTracker.addClient(requests[0].Key);
                    NetworkInterface.send(new PlayerRequestResponse(true), requests[0].Key);
                    Game1.removeGameComponent(this);
                    Game1.addGameComponent(new WaitingMode(requests[0].Key));
                }
                else if (keyboardButtons.keyPresses[Keys.Escape] == KeyboardInput.KeyboardActions.Pressed)
                {
                    NetworkInterface.send(new PlayerRequestResponse(false), requests[0].Key);
                    requests.RemoveAt(0);
                }

            }
            else
            {
                if (keyboardButtons.keyPresses[Keys.Escape] == KeyboardInput.KeyboardActions.Pressed)
                {
                    NavigationHelper.goToMenu();
                }
            }
        }

        public void playerRequestEventHandler(object sender, PlayerRequestArgs e)
        {
            if (requests.Where(req => req.Key.Equals(e.ip)).Count() == 0)
            {
                requests.Add(new KeyValuePair<IPAddress, String>(e.ip, e.machineName));
            }
        }
    }
}
