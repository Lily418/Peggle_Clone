using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Networking;
using System.Diagnostics;
using Helper;

namespace Peggle
{
    class ShutdownHandler
    {
        public ShutdownHandler()
        {
            PacketEvents.clientShutdown += shutdownEventHandler;
        }

        public void shutdownEventHandler(object sender, ClientShutdownArgs e)
        {
            NavigationHelper.goToMenu();
            new Alert("Player Exited", new Vector2(DrawHelper.getInstance().centerX("Player Exited"), 450), TimeSpan.FromSeconds(5), Color.Red); 
        }
    }
}
