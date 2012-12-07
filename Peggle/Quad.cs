using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Helper;

namespace Peggle
{
    public class Quad : Shape
    {
        public Vector2 topLeft     { private set; get; }
        public Vector2 topRight    { private set; get; }
        public Vector2 bottomLeft  { private set; get; }
        public Vector2 bottomRight { private set; get; }

        public Line top    { private set; get; }
        public Line bottom { private set; get; }
        public Line right  { private set; get; }
        public Line left   { private set; get; }

        public float minX {private set; get;}
        public float minY {private set; get;}
        public float maxX { private set; get; }
        public float maxY { private set; get; }
        
        public float width
        {
            get
            {
                return maxX - minX;
            }
        }

        public float height
        {
            get
            {
                return maxY - minY;
            }
        }

        public Vector2 center { private set; get; }
       

        public Quad(Vector2 topLeft, Vector2 topRight, Vector2 bottomLeft, Vector2 bottomRight)
        {
            this.topLeft = topLeft;
            this.topRight = topRight;
            this.bottomLeft = bottomLeft;
            this.bottomRight = bottomRight;

            top = Line.getLineFromPoints(topLeft, topRight);
            bottom = Line.getLineFromPoints(bottomLeft, bottomRight);
            left = Line.getLineFromPoints(topLeft, bottomLeft);
            right = Line.getLineFromPoints(topRight, bottomRight);

            float[] pointsX = new float[]{topLeft.X, topRight.X, bottomLeft.X, bottomRight.X};
            float[] pointsY = new float[]{topLeft.Y, topRight.Y, bottomLeft.Y, bottomRight.Y};

            minX = MyMathHelper.min(pointsX);
            minY = MyMathHelper.min(pointsY);
            maxX = MyMathHelper.max(pointsX);
            maxY = MyMathHelper.max(pointsY);

            center = new Vector2(minX + width / 2, minY + height / 2);
        }

        public static Quad organiseQuadPoints(Vector2[] points)
        {
            Array.Sort(points, new VectorComparer(VectorComparer.Axis.x));

            Vector2 topLeft;
            Vector2 bottomLeft;
            Vector2 topRight;
            Vector2 bottomRight;

            if (points[0].Y < points[1].Y)
            {
                topLeft = points[0];
                bottomLeft = points[1];
            }
            else
            {
                topLeft = points[1];
                bottomLeft = points[0];
            }

            if (points[2].Y < points[3].Y)
            {
                topRight = points[2];
                bottomRight = points[3];
            }
            else
            {
                topRight = points[3];
                bottomRight = points[2];
            }


            return new Quad(topLeft, topRight, bottomLeft, bottomRight);
        }

        public bool pointInQuad(int x, int y)
        {
            if (x < minX || x > maxX || y > maxY || y < minY)
            {
                return false;
            }

            float topY = top.yFromX(x);
            if (y < topY)
            {
                return false;
            }

            float bottomY = bottom.yFromX(x);
            if (y > bottomY)
            {
                return false;
            }

            float leftX = left.xFromY(y);
            if (x < leftX)
            {
                return false;
            }

            float rightX = right.xFromY(y);
            if (x > rightX)
            {
                return false;
            }

            return true;
        }


        public void draw(Color color)
        {
            DrawHelper dh = DrawHelper.getInstance();

            float[] pointsX = new float[] {topLeft.X, topRight.X, bottomLeft.X, bottomRight.X} ;
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
                    if(pointInQuad(x,y))
                    {
                        Rectangle drawPosition = new Rectangle(x, y, 1, 1);
                        dh.sb.Draw(dh.dummyTexture, drawPosition, color);
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
