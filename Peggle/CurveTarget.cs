using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Helper;

namespace Peggle
{
    class CurveTarget : Target
    {
        CurvedBrick position;
        BitArray drawPositions;

        public CurveTarget(CurvedBrick position, Game1 game) : base(game, false)
        {
            this.position = position;
            float minX = MyMathHelper.min(new float[] { position.lowerCurve.p0.X, position.lowerCurve.p1.X, position.lowerCurve.p2.X, position.upperCurve.p0.X, position.upperCurve.p1.X, position.upperCurve.p2.X });
            float minY = MyMathHelper.min(new float[] { position.lowerCurve.p0.Y, position.lowerCurve.p1.Y, position.lowerCurve.p2.Y, position.upperCurve.p0.Y, position.upperCurve.p1.Y, position.upperCurve.p2.Y });
            float maxX = MyMathHelper.max(new float[] { position.lowerCurve.p0.X, position.lowerCurve.p1.X, position.lowerCurve.p2.X, position.upperCurve.p0.X, position.upperCurve.p1.X, position.upperCurve.p2.X });
            float maxY = MyMathHelper.max(new float[] { position.lowerCurve.p0.Y, position.lowerCurve.p1.Y, position.lowerCurve.p2.Y, position.upperCurve.p0.Y, position.upperCurve.p1.Y, position.upperCurve.p2.Y });
            int width  = (int)Math.Ceiling(MathHelper.Distance(minX, maxX));
            int height = (int)Math.Ceiling(MathHelper.Distance(minY, maxY));
            drawPositions = new BitArray(width * height);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    //float widthRatio = (float)i / (float)width;
                    //float heightRatio = (float)i / (float)height;
                    //Vector2 
                    //drawPositions[ArrayHelper.twoIndexesToOne(j, i, width)] = false;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {

        }

        public override Shape boundingBox()
        {
            return position;
        }
    }
}
