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
        QuadCollection quads;

        public CurveTarget(CurvedBrick position, Game game) : base(game, false)
        {
            quads = position.quads;
        }

        public override void Draw(GameTime gameTime)
        {
            quads.Draw(gameTime);
        }

        public override Shape boundingBox()
        {
            return quads;
        }
    }
}
