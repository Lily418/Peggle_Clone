using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Peggle
{
    class Quad
    {
        Vector2 topLeft;
        Vector2 topRight;
        Vector2 bottomLeft;
        Vector2 bottomRight;

        public Quad(Vector2 topLeft, Vector2 topRight, Vector2 bottomLeft, Vector2 bottomRight)
        {
            this.topLeft = topLeft;
            this.topRight = topRight;
            this.bottomLeft = bottomLeft;
            this.bottomRight = bottomRight;
        }

        public void draw()
        {
            DrawHelper dh = DrawHelper.getInstance();

            Line top    = Line.getLineFromPoints(topLeft, topRight);
            Line left   = Line.getLineFromPoints(topLeft, bottomLeft);
            Line right  = Line.getLineFromPoints(topRight, bottomRight);
            Line bottom = Line.getLineFromPoints(bottomLeft, bottomRight);

            int minX = Math.Min((int)topLeft.X,    (int)bottomLeft.X);
            int minY = Math.Min((int)topLeft.Y,    (int)topRight.Y);
            int maxX = Math.Max((int)topRight.X,   (int)bottomRight.X);
            int maxY = Math.Max((int)bottomLeft.Y, (int)bottomRight.Y);

            dh.sb.Begin();


            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {


                    
                    float topY = top.yFromX(x);

                    if ((float)y < topY)
                    {
                        continue;
                    }
                    

                    

                    float bottomY = bottom.yFromX(x);
                    if ((float)y > bottomY)
                    {
                        continue;
                    }

                  


                    


                    float leftX = left.xFromY(y);

                    if (x == 81 && y == 35)
                    {
                        Debug.WriteLine(leftX);
                    }

                    if ((float)x < leftX)
                    {
                        continue;
                    }

                   
                    
                     

                    float rightX = right.xFromY(y);
                    if ((float)x > rightX)
                    {
                        continue;
                    }

                    Rectangle drawPosition = new Rectangle(x, y, 1, 1);
                    dh.sb.Draw(dh.dummyTexture, drawPosition, Color.DeepPink);

                }
            }

            dh.sb.End();
        }

        public override string ToString()
        {
            return "Top Left : " + topLeft + " Top Right : " + topRight + " Bottom Left : " + bottomLeft + " Bottom Right : " + bottomRight;
        }

    }
}
