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

        public CircularTarget(Game game, Location location)
            : base(game)
        {
            this.location = location;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            DrawHelper drawHelper = DrawHelper.getInstance();
            SpriteBatch sb = drawHelper.sb;
            sb.Begin();
            sb.Draw(drawHelper.circleTexture, location.asRectangle(), Color.White);
            sb.End();
            
        }

        public Shape boundingBox()
        {
            return Circle.circleFromLocation(location);
        }
    }
}
