using Microsoft.Xna.Framework;

namespace Peggle
{
    public interface IEntity
    {
        Shape boundingBox();
        bool enabled();
    }
}
