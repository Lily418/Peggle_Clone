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

            Vector2[] points = new Vector2[4];
            points[0] = topLeft;
            points[1] = topRight;
            points[2] = bottomLeft;
            points[3] = bottomRight;

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

  //int      i, j=polySides-1 ;
  //boolean  oddNodes=NO      ;

  //for (i=0; i<polySides; i++) {
  //  if (polyY[i]<y && polyY[j]>=y
  //  ||  polyY[j]<y && polyY[i]>=y) 
      {
  //    if (polyX[i]+(y-polyY[i])/(polyY[j]-polyY[i])*(polyX[j]-polyX[i])<x) 
          {
  //      oddNodes=!oddNodes; 
          }
      
      }
  //  j=i; }

  //return oddNodes; }


                    int j = 3;
                    bool odd = false;

                    for (int i = 0; i < points.Length; i++)
                    {
                        if (points[i].Y < y && points[j].Y >= y
                           || points[j].Y < y && points[i].Y >= y)
                        {
                            if(points[i].X + (y - points[i].Y) / (points[j].Y - points[i].Y) * (points[j].X - points[i].X) < x)
                            {
                                odd = !odd;
                            }
                        }

                        j = i;
                    }


                    if (odd)
                    {
                        Rectangle drawPosition = new Rectangle(x, y, 1, 1);
                        dh.sb.Draw(dh.dummyTexture, drawPosition, Color.DeepPink);
                    }
                    

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
