﻿using System;
using Microsoft.Xna.Framework;
using Helper;

namespace Peggle
{
    //Quadratic Bézier curve
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
            float x = (float)(Math.Pow((1f - t), 2.0) * p0.X + 2 * (1 - t) * t * p1.X + Math.Pow(t, 2) * p2.X);
            float y = (float)(Math.Pow((1f - t), 2.0) * p0.Y + 2 * (1 - t) * t * p1.Y + Math.Pow(t, 2) * p2.Y);

            return new Vector2(x, y);
        }

        public void draw(Color color)
        {
            DrawHelper dh = DrawHelper.getInstance();

            float interval = 1f / Vector2.Distance(p0, p2); 

            dh.sb.Begin();
            for (float i = 0f; i < 1f; i += interval)
            {
                Vector2 point = getPoint(i);
                Rectangle drawPosition = new Rectangle((int)point.X, (int)point.Y, 2, 2);
                dh.sb.Draw(dh.dummyTexture, drawPosition, color);
                
            }
            dh.sb.End();
        }
    }
}
