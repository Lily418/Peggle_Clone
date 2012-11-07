using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Peggle
{
    class ShooterInstructions
    {
        public float movementDirection { get; private set; }

        public ShooterInstructions(float movementDirection)
        {
            this.movementDirection = movementDirection;
        }

    }
}
