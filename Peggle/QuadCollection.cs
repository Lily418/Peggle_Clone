using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Peggle
{
    class QuadCollection : DrawableGameComponent
    {
        List<Quad> quads = new List<Quad>();

        public QuadCollection(Game game)
            : base(game)
        {
        }

        public void addQuad(Quad newQuad)
        {
            quads.Add(newQuad);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Quad quad in quads)
            {
                quad.draw();
            }
        }
    }
}
