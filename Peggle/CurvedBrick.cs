using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Helper;

namespace Peggle
{
    class CurvedBrick : Shape
    {
        public Curve upperCurve { get; private set; }
        public Curve lowerCurve { get; private set; }

        public CurvedBrick(Curve upperCurve, Curve lowerCurve)
        {
            this.upperCurve = upperCurve;
            this.lowerCurve = lowerCurve;
        }

        public QuadCollection toQuads()
        {

            QuadCollection curveQuads = new QuadCollection(Game1.game);

            float intervalUpper = calculateInterval(upperCurve);
            float intervalLower = calculateInterval(lowerCurve);

            float usedInterval = Math.Min(intervalLower, intervalUpper);

            for (float i = 0.0f; i < 1.0f; i += usedInterval / 5)
            {
                Vector2 p0   = upperCurve.getPoint(i);
                Vector2 p1   = upperCurve.getPoint(i + usedInterval);
                Vector2 p2   = lowerCurve.getPoint(i);
                Vector2 p3   = lowerCurve.getPoint(i + usedInterval);

                Quad quad;
                curveQuads.addQuad(quad = ShapeHelper.organiseQuadPoints(new Vector2[]{p0, p1, p2, p3}));
            }

            return curveQuads;
        }


        private float calculateInterval(Curve curve)
        {
            //const float INTERVAL_BETWEEN_POINTS = 50f;
            //return INTERVAL_BETWEEN_POINTS / curve.getLength();

            return 0.2f;
        }
    }
}
