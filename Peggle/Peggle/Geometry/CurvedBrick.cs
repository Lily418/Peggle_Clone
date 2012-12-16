using Microsoft.Xna.Framework;

namespace Peggle
{
    public class CurvedBrick
    {
        public Curve upperCurve { private set; get; }
        public Curve lowerCurve { private set; get; }
        public QuadCollection quads { private set; get; }

        public CurvedBrick(Curve upperCurve, Curve lowerCurve)
        {
            this.upperCurve = upperCurve;
            this.lowerCurve = lowerCurve;
            quads = toQuads();
        }

        private QuadCollection toQuads()
        {

            QuadCollection curveQuads = new QuadCollection();

            const float interval = 0.1f;

            for (float i = 0.0f; i < 1.0f; i += interval)
            {
                Vector2 p0   = upperCurve.getPoint(i);
                Vector2 p1   = upperCurve.getPoint(i + interval);
                Vector2 p2   = lowerCurve.getPoint(i);
                Vector2 p3   = lowerCurve.getPoint(i + interval);

                curveQuads.addQuad(Quad.organiseQuadPoints(new Vector2[]{p0, p1, p2, p3}));
            }

            return curveQuads;
        }
    }
}
