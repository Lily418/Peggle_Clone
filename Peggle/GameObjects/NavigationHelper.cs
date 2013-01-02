using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Peggle
{
    static class NavigationHelper
    {
        public static void goToMenu()
        {
            EventHandlers.resetEventHandlers();
            Game1.clearGameComponents();
            Game1.addGameComponent(new SetupMenu());
        }
    }
}
