using Microsoft.Xna.Framework;
using Helper;

namespace Peggle
{
    public class CurveTarget : Target, IShallowClone<CurveTarget>
    {
        CurvedBrick position;
        Line leftLine;
        Line rightLine;

        public CurveTarget(CurvedBrick position) : base()
        {
            this.position = position;
            leftLine  = Line.getLineFromPoints(position.upperCurve.p0, position.lowerCurve.p0);
            rightLine = Line.getLineFromPoints(position.upperCurve.p2, position.lowerCurve.p2);
        }

        public override void Draw(GameTime gameTime)
        {
            Color innerColor = color.increaseBrightness(20);

            if (hit)
            {
                innerColor.increaseBrightness(50);
            }

            position.quads.Draw(innerColor);
            position.upperCurve.draw(color);
            position.lowerCurve.draw(color);
            leftLine.draw(color);
            rightLine.draw(color);
        }

        public override Shape boundingBox()
        {
            return position.quads;
        }

        public CurveTarget clone()
        {
            return new CurveTarget(position);
        }
    }
}
