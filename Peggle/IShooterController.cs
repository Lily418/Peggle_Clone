using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Peggle
{
    public interface IShooterController
    {
        ShooterInstructions getShooterInstructions(GameTime gameTime, Shooter shooter);
    }
}
