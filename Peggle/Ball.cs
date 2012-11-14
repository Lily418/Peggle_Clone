using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Peggle
{
    class Ball : DrawableGameComponent, IEntityPhysics
    {
        static Texture2D texture;
        Color color;
        public Location location { get; set; }

        public Vector2 velocity { get; set; }
        public float maxSpeed { get; set; }

        public Ball(Game game, Location startingLocation, float angle) : base (game)
        {
            location = startingLocation;
            color = RandomHelper.randomColor();

            if (texture == null)
            {
                DrawHelper drawHelper = DrawHelper.getInstance();
                texture = drawHelper.circleTexture;
            }

            velocity = new PolarCoordinate(10.0f, angle).toCartesian();
            

            maxSpeed = 2;

       
            


        }

        public override void Update(GameTime gameTime)
        {
            if (Game1.graphics.GraphicsDevice.Viewport.Bounds.Bottom < location.topLeft.toPoint().Y)
            {
                EventHanders.raiseEvent(new BallFallenArgs(), EventType.BallFallen);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            DrawHelper drawHelper = DrawHelper.getInstance();
            SpriteBatch sb = drawHelper.sb;

            sb.Begin();
            sb.Draw(texture, location.asRectangle(), color);
            sb.End();
        }



        public Shape boundingBox()
        {
            return Circle.circleFromLocation(location);
        }
    }
}
