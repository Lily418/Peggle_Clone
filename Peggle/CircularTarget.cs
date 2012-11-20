using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Helper;

namespace Peggle
{
    class CircularTarget : Target
    {
        public Location location { get; private set; }
        

        public CircularTarget(Game game, Location location, bool countsTowardsLevelProgress)
            : base(game, countsTowardsLevelProgress)
        {
            this.location = location;
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

        public override Shape boundingBox()
        {
            return Circle.circleFromLocation(location);
        }
    }
}
