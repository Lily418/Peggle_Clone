using Microsoft.Xna.Framework;

namespace Peggle
{
    public interface Shape
    {
        void translate(Vector2 direction);
        float leftMostPoint();
        float rightMostPoint();
    }
}
