using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Helper;

namespace Peggle
{
    public class Circle : Shape
    {
        public Vector2 origin { private set; get; }
        public float radius { private set; get; }
        public Vector2 top
        {
            get
            {
                return new Vector2(origin.X, origin.Y - radius);
            }
        }

        public Circle(Vector2 origin, float radius)
        {
            this.origin = origin;
            this.radius = radius;
        }

        public void translate(Vector2 direction)
        {
            origin += direction;
        }

        public void draw(Color color)
        {
            DrawHelper drawHelper = DrawHelper.getInstance();
            SpriteBatch sb = drawHelper.sb;

            sb.Begin();
            Rectangle drawPosition = new Rectangle((int)(origin.X - radius), (int)(origin.Y - radius), (int)(radius * 2f), (int)(radius * 2f));
            sb.Draw(drawHelper.circleTexture, drawPosition, color);
            sb.End();
        }


        public float leftMostPoint()
        {
            return origin.X - radius;
        }

        public float rightMostPoint()
        {
            return origin.X + radius;
        }
    }
}
