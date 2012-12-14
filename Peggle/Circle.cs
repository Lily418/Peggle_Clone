using Microsoft.Xna.Framework;
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
            DrawHelper dh = DrawHelper.getInstance();

            dh.sb.Begin();
            Rectangle drawPosition = new Rectangle((int)(origin.X - radius), (int)(origin.Y - radius), (int)(radius * 2f), (int)(radius * 2f));
            dh.sb.Draw(dh.circleTexture, drawPosition, color);
            dh.sb.End();
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
