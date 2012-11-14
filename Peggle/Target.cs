using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Peggle
{
    abstract class Target : DrawableGameComponent
    {
        protected bool hit = false;
        protected Game1 game;

        protected Target(Game game)
            : base(game)
        {
            EventHanders.collision  += collisionEventHandler;
            EventHanders.ballFallen += ballFallenEventHandler;
            this.game = (Game1)game;
        }

        public void collisionEventHandler(object sender, CollisionArgs e)
        {
            if (e.hitObject != null && e.hitObject.Equals(this))
            {
                hit = true;
            }
        }

        public void ballFallenEventHandler(object sender, BallFallenArgs e)
        {
            if (hit)
            {
                game.removeGameComponent(this);
            }
        }

       

   
    }
}
