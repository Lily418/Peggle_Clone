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
    public class CurveTarget : Target, IShallowClone<CurveTarget>
    {
        CurvedBrick position;
        Line leftLine;
        Line rightLine;

        public CurveTarget(CurvedBrick position, Color color, Game game) : base(game, color)
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
            leftLine.draw ();
            rightLine.draw();
        }

        public override Shape boundingBox()
        {
            return position.quads;
        }

        public CurveTarget clone()
        {
            return new CurveTarget(position, color, game);
        }
    }
}
