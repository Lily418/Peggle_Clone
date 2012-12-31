using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Peggle;
using Helper;
using System.Net;
using System.Diagnostics;

namespace Networking
{
    class WaitingMode : DrawableGameComponent
    {
        List<Shooter> shooters = new List<Shooter>();
        IPAddress serverIP;
        DateTime lastSent = DateTime.Now;
        public WaitingMode(IPAddress serverIP) : base(Game1.game)
        {
            this.serverIP = serverIP;
            PacketEvents.setup += setupRequestEventHandler;
            Dialog.gainControl(this);
        }

        public override void Update(GameTime gameTime)
        {
            if (DateTime.Now - lastSent > TimeSpan.FromSeconds(5))
            {
                NetworkInterface.send(new ResendRequestPacket(), serverIP);
                lastSent = DateTime.Now;
            }

        }

        public void setupRequestEventHandler(object sender, SetupArgs e)
        {
            foreach (uint identifier in e.shooterIdentfiers)
            {
                if (identifier != e.clientIdentfier)
                {
                    shooters.Add(new Shooter(Color.Red, new NetworkShooter(serverIP, identifier), "Shooter 1"));
                }
                else
                {
                    Shooter playerShooter;
                    shooters.Add(playerShooter = new Shooter(Color.Blue, PlayerInput.getInstance(), "Player"));
                    playerShooter.server = serverIP;
                }
            }

            Game1.setLevelManager(new LevelStateManager(shooters));
            Game1.levelStateManager.loadLevel();
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
