using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Helper;
using System.Diagnostics;

namespace Peggle
{
    class Score : DrawableGameComponent
    {
        Shooter shooter;
        public int score { private set; get; }
        List<IEntity> targetsHit = new List<IEntity>();
        Vector2 drawRatio;
        public String label { private set; get; }

        public Score(Shooter shooter, Vector2 drawRatio, String label) : base(Game1.game)
        {
            this.score = 0;
            this.shooter = shooter;
            this.label = label;
            this.drawRatio = drawRatio;
            EventHandlers.collision += collisionEventHandler;
        }

        public void collisionEventHandler(object sender, CollisionArgs e)
        {   
            if (e.collidingObject is Ball && ((Ball)e.collidingObject).shotBy == shooter && e.hitObject is Target)
            {
                if (!targetsHit.Contains(e.hitObject))
                {
                    score += Target.POINTS;
                    targetsHit.Add(e.hitObject);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            DrawHelper dh = DrawHelper.getInstance();

            dh.sb.Begin();

            Rectangle viewport = Game1.graphics.GraphicsDevice.Viewport.Bounds;
            float x = viewport.Width  * drawRatio.X;
            float y = viewport.Height * drawRatio.Y;

            dh.sb.DrawString(dh.font, label +" "+ score , new Vector2(x,y) , Color.Wheat);

            dh.sb.End();

        }
    }
}
