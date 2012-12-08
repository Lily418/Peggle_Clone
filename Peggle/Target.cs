using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Helper;

namespace Peggle
{
    public abstract class Target : DrawableGameComponent, Entity
    {
        public const int POINTS = 50;
        public static readonly Color defaultColor = Color.Blue;
        public bool hit { get; private set; }
        protected Game1 game;
        protected Color color;

        protected Target(Game game, Color color)
            : base(game)
        {
            this.game = (Game1)game;
            this.hit = false;
            EventHandlers.collision  += collisionEventHandler;
            EventHandlers.ballFallen += ballFallenEventHandler;

            this.color = color;
            
        }

        public void collisionEventHandler(object sender, CollisionArgs e)
        {
            if (!hit)
            {
                if (e.hitObject != null && e.hitObject.Equals(this) && e.collidingObject is Ball)
                {
                    Ball ball = (Ball)e.collidingObject;

                    if (!ball.isSimulation)
                    {
                        hit = true;
                        color = color.increaseBrightness(80);

                    }
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
