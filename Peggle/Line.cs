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

        public void draw(int X1, int X2, int Y1, int Y2)
        {
            const int LINE_THICKNESS = 2;
            //Debug.WriteLine(m + " " + c);
            DrawHelper dh = DrawHelper.getInstance();

            dh.sb.Begin();

                int startX = Math.Min(X1, X2);
                int endX   = Math.Max(X1, X2);


                if (startX != endX)
                {
                    for (int x = startX; x < endX; x++)
                    {
                        int y = (int)yFromX(x);
                        Rectangle drawPosition = new Rectangle(x, y, LINE_THICKNESS, LINE_THICKNESS);
                        dh.sb.Draw(dh.dummyTexture, drawPosition, Color.Purple);
                    }
                }
                else
                {

                    int startY = Math.Min(Y1, Y2);
                    int endY   = Math.Max(Y1, Y2);

                    for (int y = startY; y < endY; y++)
                    {

                        Rectangle drawPosition = new Rectangle(X1, y, LINE_THICKNESS, LINE_THICKNESS);
                        dh.sb.Draw(dh.dummyTexture, drawPosition, Color.Purple);
                    }
                }

                
            

            dh.sb.End();
        }
    }
}
