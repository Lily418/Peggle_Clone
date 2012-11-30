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
        QuadCollection quads;

        public CurveTarget(CurvedBrick position, Game game) : base(game, false)
        {
           
            quads = position.quads;
        }

        public CurveTarget(QuadCollection collection, Game game)
            : base(game, false)
        {
            quads = collection;
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
