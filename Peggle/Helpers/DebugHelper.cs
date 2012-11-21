using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Peggle;

namespace Helper
{
    static class DebugHelper
    {
        public static void drawPoint(Vector2 point)
        {
            DrawHelper dh = DrawHelper.getInstance();

            dh.sb.Begin();
            dh.sb.Draw(dh.dummyTexture, new Rectangle((int)point.X, (int)point.Y,2,2), Color.Aqua);
            dh.sb.End();
        }
    }
}
