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
        public Vector2 a { private set; get; }
        public Vector2 b { private set; get; }
        public float   m { get; private set; }
        public float   c { get; private set; }

        private Line(float m, float c, Vector2 a, Vector2 b)
        {
            this.m = m;
            this.c = c;
            this.a = a;
            this.b = b;

        }

        public static Line getLineFromPoints(Vector2 a, Vector2 b)
        {

            float m = (a.Y - b.Y) / (a.X - b.X);

            float c = a.Y - (m * a.X);

            return new Line(m, c, a, b);
        }

        public float yFromX(float x)
        {
            return m * x + c;
        }

        public float xFromY(float y)
        {

            float x = (y - c) / m;

            //Dealing with vertical lines
            if (float.IsNaN(x))
            {
                x = a.X;
            }

            return x;
        }

        public void draw(Color color)
        {

            const int LINE_THICKNESS = 2;

            DrawHelper dh = DrawHelper.getInstance();

            dh.sb.Begin();

            int startX = Math.Min((int)a.X, (int)b.X);
            int endX   = Math.Max((int)a.X, (int)b.X);
            int startY = Math.Min((int)a.Y, (int)b.Y);
            int endY   = Math.Max((int)a.Y, (int)b.Y);

            for (int x = startX; x < endX; x++)
            {
                int y = (int)yFromX(x);
                Rectangle drawPosition = new Rectangle(x, y, LINE_THICKNESS, LINE_THICKNESS);
                dh.sb.Draw(dh.dummyTexture, drawPosition, color);
            }

            for (int y = startY; y < endY; y++)
            {
                int x = (int)xFromY(y);
                Rectangle drawPosition = new Rectangle(x, y, LINE_THICKNESS, LINE_THICKNESS);
                dh.sb.Draw(dh.dummyTexture, drawPosition, color);
            }

            dh.sb.End();
        }
    }
}
