using Microsoft.Xna.Framework;

namespace Peggle
{
    public interface IEntityPhysics : IEntity
    {
        Vector2 velocity { get; set; }
        float maxSpeed { get; set; }
    }
}
