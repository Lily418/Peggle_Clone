using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Peggle
{
    static class Dialog
    {
        public static void gainControl(GameComponent controlTaker)
        {
            foreach (GameComponent gc in Game1.getComponents())
            {
                if (gc != controlTaker)
                {
                    gc.Enabled = false;
                    if (gc is DrawableGameComponent)
                    {
                        ((DrawableGameComponent)gc).Visible = false;
                    }
                }

            }
        }

        public static void returnControl(GameComponent controlGiver)
        {
            foreach (GameComponent gc in Game1.getComponents())
            {
                if (gc != controlGiver)
                {
                    gc.Enabled = true;
                    if (gc is DrawableGameComponent)
                    {
                        ((DrawableGameComponent)gc).Visible = true;
                    }
                }

            }
        }
    }
}
