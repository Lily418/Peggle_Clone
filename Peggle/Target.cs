﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Peggle
{
    abstract class Target : DrawableGameComponent, Entity
    {
        public bool hit { get; private set; }
        public bool countsTowardsLevelProgress { get; private set; }
        protected Game1 game;

        protected Target(Game game, bool countsTowardsLevelProgress)
            : base(game)
        {
            this.game = (Game1)game;
            this.hit = false;
            this.countsTowardsLevelProgress = countsTowardsLevelProgress;
            EventHandlers.collision  += collisionEventHandler;
            EventHandlers.ballFallen += ballFallenEventHandler;
            
        }

        public void collisionEventHandler(object sender, CollisionArgs e)
        {
            if (e.hitObject != null && e.hitObject.Equals(this) && e.collidingObject is Ball)
            {
                Ball ball = (Ball)e.collidingObject;

                if (!ball.isSimulation)
                {
                    hit = true;
                }
            }
        }

        public void ballFallenEventHandler(object sender, BallFallenArgs e)
        {
            
            if (hit)
            {
                EventHandlers.collision -= collisionEventHandler;
                EventHandlers.ballFallen -= ballFallenEventHandler;
                Game1.removeGameComponent(this);
            }
        }

        public abstract Shape boundingBox();

       

   
    }
}
