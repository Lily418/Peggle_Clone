using Microsoft.Xna.Framework;
using Helper;
using System.Diagnostics;

namespace Peggle
{
    public abstract class Target : DrawableGameComponent, IEntity
    {
        protected Color color;
        public const int POINTS = 50;
        public bool hit { get; private set; }

        protected Target():base(Game1.game)
        {
            hit = false;
            EventHandlers.getInstance().collision += collisionEventHandler;
            EventHandlers.getInstance().ballFallen += ballFallenEventHandler;
            
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
                EventHandlers.getInstance().collision -= collisionEventHandler;
                EventHandlers.getInstance().ballFallen -= ballFallenEventHandler;
            }
        }

        public void setColor(Color color)
        {
            this.color = color;
        }

        public bool enabled()
        {
            return base.Enabled;
        }

        public abstract Shape boundingBox();

    }
}
