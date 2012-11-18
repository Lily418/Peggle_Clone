using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Peggle
{
    class CurvedBrick : Shape
    {
        public Curve upperCurve { get; private set; }
        public Curve lowerCurve { get; private set; }

        public QuadCollection toQuads()
        {

            QuadCollection curveQuads = new QuadCollection();

            float intervalUpper = calculateInterval(upperCurve);
            float intervalLower = calculateInterval(lowerCurve);

            float usedInterval = Math.Min(intervalLower, intervalUpper);

            for (float i = 0.0f; i < 1.0f; i += (usedInterval * 2))
            {
                Vector2 topLeft     = upperCurve.getPoint(i);
                Vector2 topRight    = upperCurve.getPoint(i + usedInterval);
                Vector2 bottomLeft  = lowerCurve.getPoint(i);
                Vector2 bottomRight = upperCurve.getPoint(i + usedInterval);

                curveQuads.addQuad(new Quad(topLeft, topRight, bottomLeft, bottomRight));
            }

            return curveQuads;
        }


        private float calculateInterval(Curve curve)
        {
            const float INTERVAL_BETWEEN_POINTS = 1f;
            return INTERVAL_BETWEEN_POINTS / curve.getLength();
        }
    }
}
