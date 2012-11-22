using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Peggle
{
    class QuadCollection : Shape
    {
        public List<Quad> quads { get; private set; }

        public QuadCollection()
        {
            quads = new List<Quad>();
        }

        public void addQuad(Quad newQuad)
        {
            quads.Add(newQuad);
        }

        public void Draw(GameTime gameTime)
        {
            foreach (Quad quad in quads)
            {
                quad.draw();
            }
        }
    }
}
