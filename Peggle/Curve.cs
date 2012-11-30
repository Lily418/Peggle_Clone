using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Helper;

namespace Peggle
{
    public class Curve
    {
        public Vector2 p0 { get; private set; }
        public Vector2 p1 { get; private set; }
        public Vector2 p2 { get; private set; }

        public Curve(Vector2 p0, Vector2 p1, Vector2 p2)
        {
            this.p0 = p0;
            this.p1 = p1;
            this.p2 = p2;
        }

        public Vector2 getPoint(float t)
        {
            float x = (float)(Math.Pow((double)(1f - t), 2.0) * p0.X + 2 * (1 - t) * t * p1.X + Math.Pow(t, 2) * p2.X);
            float y = (float)(Math.Pow((double)(1f - t), 2.0) * p0.Y + 2 * (1 - t) * t * p1.Y + Math.Pow(t, 2) * p2.Y);

            return new Vector2(x, y);
        }

        public float getLength()
        {
            return Vector2.Distance(p0, p1) + Vector2.Distance(p1, p2);
        }

        public void draw()
        {
            DrawHelper dh = DrawHelper.getInstance();

            float interval = 1f / Vector2.Distance(p0, p2); 

            dh.sb.Begin();
            for (float i = 0f; i < 1f; i += interval)
            {
                Vector2 point = getPoint(i);
                Rectangle drawPosition = new Rectangle((int)point.X, (int)point.Y, 1, 1);
                dh.sb.Draw(dh.dummyTexture, drawPosition, Color.Yellow);
                
            }
            dh.sb.End();
        }
    }
}
