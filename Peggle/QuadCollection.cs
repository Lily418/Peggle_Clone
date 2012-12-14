using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Peggle
{
    public class QuadCollection : Shape
    {
        public List<Quad> quads { get; private set; }
        float left;
        float right;

        public QuadCollection()
        {
            quads = new List<Quad>();
        }

        public void addQuad(Quad newQuad)
        {
            quads.Add(newQuad);

            if (newQuad.leftMostPoint() < left)
            {
                left = newQuad.leftMostPoint();
            }

            if (newQuad.rightMostPoint() > right)
            {
                right = newQuad.rightMostPoint();
            }
        }

        public void Draw(GameTime gameTime, Color color)
        {
            foreach (Quad quad in quads)
            {
                quad.draw(color);
            }
        }

        public void translate(Vector2 direction)
        {
            foreach (Quad quad in quads)
            {
                quad.translate(direction);
            }
        }


        public float leftMostPoint()
        {
            return left;
        }

        public float rightMostPoint()
        {
            return right;
        }
    }
}
