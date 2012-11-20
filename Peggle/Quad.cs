using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Helper;

namespace Peggle
{
    class Quad
    {
        public Vector2 topLeft     { private set; get; }
        public Vector2 topRight    { private set; get; }
        public Vector2 bottomLeft  { private set; get; }
        public Vector2 bottomRight { private set; get; }

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

            //Vector2[] points = new Vector2[4];
            //points[0] = topLeft;
            //points[1] = topRight;
            //points[2] = bottomLeft;
            //points[3] = bottomRight;

            Line top    = Line.getLineFromPoints(topLeft, topRight);
            Line bottom = Line.getLineFromPoints(bottomLeft, bottomRight);
            Line left   = Line.getLineFromPoints(topLeft, topRight);
            Line right  = Line.getLineFromPoints(topRight, bottomRight);

            float[] pointsX = new float[] {topLeft.X,topRight.X,bottomLeft.X,bottomRight.X} ;
            float[] pointsY = new float[] {topLeft.Y, topRight.Y, bottomLeft.Y, bottomRight.Y };

            int minX = (int)MyMathHelper.min(pointsX);
            int minY = (int)MyMathHelper.min(pointsY);
            int maxX = (int)MyMathHelper.max(pointsX);
            int maxY = (int)MyMathHelper.max(pointsY);

            dh.sb.Begin();


            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {

                    float topY = top.yFromX(x);
                    if(y < topY)
                    {
                        continue;
                    }

                    float bottomY = bottom.yFromX(x);
                    if (y > bottomY)
                    {
                        continue;
                    }

                    float leftX = left.xFromY(y);
                    if(x < leftX)
                    {
                        continue;
                    }

                    float rightX = right.xFromY(y);
                    if(x > rightX)
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

        private bool pointInPolygon(Vector2[] points, Vector2 test)
        {
  bool c = false;
  int i, j;
  for (i = 0, j = points.Length-1; i < points.Length; j = i++) {
    if ( ((points[i].Y> test.Y) != (points[j].Y>test.Y)) &&
	 (test.X < (points[j].X-points[i].X) * (test.Y-points[i].Y) / (points[j].Y-points[i].Y) + points[i].X) )
       c = !c;
  }
  return c;
        }

    }
}
