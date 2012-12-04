using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Helper;


namespace Peggle
{
    public class Line
    {
        public float m { get; private set; }
        public float c { get; private set; }

        private Line(float m, float c)
        {
            this.m = m;
            this.c = c;
        }

        public static Line getLineFromPoints(Vector2 a, Vector2 b)
        {
            float m = (a.Y - b.Y) / (a.X - b.X);
            
            float c = a.Y - (m * a.X);

            return new Line(m, c);
        }

        public float yFromX(float x)
        {
            //y = mx + c
            return m * x + c;
        }

        public float xFromY(float y)
        {
            return (y - c) / m;
        }

        public void draw()
        {
            DrawHelper dh = DrawHelper.getInstance();

            dh.sb.Begin();

            for (int x = 0; x < 800; x++)
            {
                int y = (int)yFromX(x);
                Rectangle drawPosition = new Rectangle(x, y, 1, 1);
                dh.sb.Draw(dh.dummyTexture, drawPosition, Color.Tomato);  
            }

            dh.sb.End();
        }
    }
}
