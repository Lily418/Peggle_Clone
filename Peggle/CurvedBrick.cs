using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Helper;

namespace Peggle
{
    class CurvedBrick
    {
        public QuadCollection quads { private set; get; }

        public CurvedBrick(Curve upperCurve, Curve lowerCurve)
        {
            quads = toQuads(upperCurve, lowerCurve);
        }

        private QuadCollection toQuads(Curve upperCurve, Curve lowerCurve)
        {

            QuadCollection curveQuads = new QuadCollection();

            float intervalUpper = calculateInterval(upperCurve);
            float intervalLower = calculateInterval(lowerCurve);

            float usedInterval = 0.1f;

            for (float i = 0.0f; i <= 1.0f; i += usedInterval)
            {
                Vector2 p0   = upperCurve.getPoint(i);
                Vector2 p1   = upperCurve.getPoint(i + usedInterval);
                Vector2 p2   = lowerCurve.getPoint(i);
                Vector2 p3   = lowerCurve.getPoint(i + usedInterval);

                curveQuads.addQuad(Quad.organiseQuadPoints(new Vector2[]{p0, p1, p2, p3}));
            }

            return curveQuads;
        }


        private float calculateInterval(Curve curve)
        {
            const float INTERVAL_BETWEEN_POINTS = 10f;
            return INTERVAL_BETWEEN_POINTS / curve.getLength();
        }
    }
}
