using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Peggle
{
    class Ball : DrawableGameComponent, IPhysicsData
    {
        static Texture2D texture;
        Color color;
        Location currentLocation;

        public Ball(Game game, Location startingLocation) : base (game)
        {
            currentLocation = startingLocation;
            color = RandomHelper.randomColor();

            if (texture == null)
            {
                DrawHelper drawHelper = DrawHelper.getInstance();
                texture = drawHelper.circleTexture;
            }

        }


        public Location getLocation()
        {
            return currentLocation;
        }

        public override void Draw(GameTime gameTime)
        {
            DrawHelper drawHelper = DrawHelper.getInstance();
            SpriteBatch sb = drawHelper.sb;

            sb.Begin();
            sb.Draw(texture, currentLocation.asRectangle(), color);
            sb.End();
        }
    }
}
