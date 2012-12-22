using Microsoft.Xna.Framework;

namespace Peggle
{
    public interface IShooterController
    {
        ShooterInstructions getShooterInstructions(GameTime gameTime, Shooter shooter);
    }
}
