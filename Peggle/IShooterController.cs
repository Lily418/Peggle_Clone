using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Peggle
{
    interface IShooterController
    {
        ShooterInstructions getShooterInstructions(TimeSpan elapsedTime);
    }
}
