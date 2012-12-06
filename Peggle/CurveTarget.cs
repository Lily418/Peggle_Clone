using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Helper;
using System.Diagnostics;

namespace Peggle
{
    public class CurveTarget : Target
    {
        CurvedBrick position;
        Line leftLine;
        Line rightLine;

        public CurveTarget(CurvedBrick position, Game game) : base(game, false)
        {
            this.position = position;
            leftLine = Line.getLineFromPoints(position.upperCurve.p0, position.lowerCurve.p0);
            rightLine = Line.getLineFromPoints(position.upperCurve.p2, position.lowerCurve.p2);

        }

        public override void Draw(GameTime gameTime)
        {
            position.quads.Draw(gameTime, color);
            position.upperCurve.draw();
            position.lowerCurve.draw();
            leftLine.draw((int)position.upperCurve.p0.X, (int)position.lowerCurve.p0.X, (int)position.upperCurve.p0.Y, (int)position.lowerCurve.p0.Y);
            rightLine.draw((int)position.upperCurve.p2.X, (int)position.lowerCurve.p2.X, (int)position.upperCurve.p2.Y, (int)position.lowerCurve.p2.Y);
        }

        public override Shape boundingBox()
        {
            return position.quads;
        }
    }
}
