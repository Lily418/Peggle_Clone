using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Peggle
{
    //Make abstract class which runs a function which asks for permission from shooter controller
    public interface IShooterController
    {
        ShooterInstructions getShooterInstructions(GameTime gameTime, Shooter shooter);
    }
}
