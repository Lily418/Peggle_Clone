using Microsoft.Xna.Framework;

namespace Peggle
{
    public class CurveTarget : Target, IShallowClone<CurveTarget>
    {
        CurvedBrick position;
        Line leftLine;
        Line rightLine;

        public CurveTarget(CurvedBrick position, Color color) : base(color)
        {
            this.position = position;
            leftLine = Line.getLineFromPoints(position.upperCurve.p0, position.lowerCurve.p0);
            rightLine = Line.getLineFromPoints(position.upperCurve.p2, position.lowerCurve.p2);

        }

        public override void Draw(GameTime gameTime)
        {
            position.quads.Draw(gameTime, color);
            position.upperCurve.draw(Color.Purple);
            position.lowerCurve.draw(Color.Purple);
            leftLine.draw(Color.Purple);
            rightLine.draw(Color.Purple);
        }

        public override Shape boundingBox()
        {
            return position.quads;
        }

        public CurveTarget clone()
        {
            return new CurveTarget(position, color);
        }
    }
}
