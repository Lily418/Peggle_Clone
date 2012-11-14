using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Peggle
{
    class CircularTarget : Target, Entity
    {
        Location location { get; set; }
        bool countsTowardsLevelProgress;

        public CircularTarget(Game game, Location location, bool countsTowardsLevelProgress)
            : base(game)
        {
            this.location = location;
            this.countsTowardsLevelProgress = countsTowardsLevelProgress;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            DrawHelper drawHelper = DrawHelper.getInstance();
            SpriteBatch sb = drawHelper.sb;

            Color color;

            if (countsTowardsLevelProgress)
            {
                color = Color.Orange;
            }
            else
            {
                color = Color.Blue;
            }

            if (hit)
            {
                color = color.increaseBrightness(80);
            }

            sb.Begin();
            sb.Draw(drawHelper.circleTexture, location.asRectangle(), color);
            sb.End();
            
        }

        public Shape boundingBox()
        {
            return Circle.circleFromLocation(location);
        }
    }
}
