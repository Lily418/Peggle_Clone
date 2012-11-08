using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Peggle
{
    abstract class Target : DrawableGameComponent, Entity
    {
        protected Target(Game game)
            : base(game)
        {
        }

        public abstract Location location
        {
            get;
            set;
        }
    }
}
