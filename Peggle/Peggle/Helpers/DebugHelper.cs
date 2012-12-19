using Microsoft.Xna.Framework;

namespace Helper
{
    static class DebugHelper
    {
        public static void drawPoint(Vector2 point, Color color)
        {
            DrawHelper dh = DrawHelper.getInstance();

            dh.sb.Begin();
            dh.sb.Draw(dh.dummyTexture, new Rectangle((int)point.X, (int)point.Y,2,2), color);
            dh.sb.End();
        }
    }
}
