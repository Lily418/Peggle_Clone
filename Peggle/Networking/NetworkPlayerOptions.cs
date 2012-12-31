using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Helper;
using System.Diagnostics;
using System.Net;
using Networking;

namespace Peggle
{
    class NetworkPlayerOptions : DrawableGameComponent
    {
        SetupMenu parent;
        IPAddress ipAddress;
        String ip = "";
        TimeSpan invalidIp = TimeSpan.Zero;

        public NetworkPlayerOptions(SetupMenu parent) : base(Game1.game)
        {
            this.parent = parent;
            Dialog.gainControl(this);
        }

        public override void Draw(GameTime gameTime)
        {
            DrawHelper dh = DrawHelper.getInstance();

            dh.sb.Begin();
            dh.sb.DrawString(dh.font, "IP:" + ip, new Vector2(180, 30), Color.White);


            dh.sb.DrawString(dh.font, "Enter - Add Player", new Vector2(dh.centerX("Enter - Add Player"), 60), Color.White);
            dh.sb.DrawString(dh.font, "Esc - Cancel",       new Vector2(dh.centerX("Esc - Cancel"),       80), Color.White);

            if (invalidIp > TimeSpan.Zero)
            {
                String invalidString = "Invalid Ip";
                dh.sb.DrawString(dh.font, invalidString, new Vector2(dh.centerX(invalidString), 300), Color.Red);
            }

            dh.sb.End();

        }

        public override void Update(GameTime gameTime)
        {
            invalidIp -= gameTime.ElapsedGameTime;

            KeyboardInput.KeyboardButtons keyboardButtons = KeyboardInput.getInstance().buttonStates;

            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                if (keyboardButtons.keyPresses[key] == KeyboardInput.KeyboardActions.Pressed)
                {
                    keyPressed(key);
                }
            }

            if (keyboardButtons.keyPresses[Keys.Enter] == KeyboardInput.KeyboardActions.Pressed)
            {
                IPAddress.TryParse(ip, out ipAddress);

                if (ipAddress != null && ipAddress.ToString() == ip)
                {

                    if (parent.playerRequests.Where(req => req.ip.Equals(ipAddress)).Count() == 0)
                    {
                        ConnectedTracker.addClient(ipAddress);
                        parent.playerRequests.Add(new PlayerRequestRecord(ipAddress, DateTime.Now));
                        NetworkInterface.send(new PlayerRequest(), ipAddress);
                        Game1.removeGameComponent(this);
                        Dialog.returnControl(this);
                    }
                    else
                    {
                        new Alert("Request Already Sent", new Vector2(DrawHelper.getInstance().centerX("Request Already Sent"), 300), TimeSpan.FromSeconds(3), Color.Red);
                    }
                }
                else
                {
                    invalidIp = TimeSpan.FromSeconds(1);
                }
            }
            else if (keyboardButtons.keyPresses[Keys.Escape] == KeyboardInput.KeyboardActions.Pressed)
            {
                Game1.removeGameComponent(this);
                Dialog.returnControl(this);
            }
        }

        private void keyPressed(Keys key)
        {
            switch (key)
            {
                case Keys.D0: ip += "0"; break;
                case Keys.D1: ip += "1"; break;
                case Keys.D2: ip += "2"; break;
                case Keys.D3: ip += "3"; break;
                case Keys.D4: ip += "4"; break;
                case Keys.D5: ip += "5"; break;
                case Keys.D6: ip += "6"; break;
                case Keys.D7: ip += "7"; break;
                case Keys.D8: ip += "8"; break;
                case Keys.D9: ip += "9"; break;
                case Keys.OemPeriod: ip += "."; break;
                case Keys.Back: if (ip.Length > 0) ip = ip.Remove(ip.Length - 1); break;
            }
        }

        public IPAddress getIp()
        {
            return ipAddress;
        }


    }
}
