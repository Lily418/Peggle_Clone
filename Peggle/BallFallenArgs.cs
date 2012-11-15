using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Peggle
{
    class BallFallenArgs : EventArgs
    {
        public Ball ball { private set; get; }

        public BallFallenArgs(Ball ball)
        {
            this.ball = ball;
        }

    }
}
