﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using Helper;

namespace Peggle
{
    public class Ball : DrawableGameComponent, IEntityPhysics
    {
        static Texture2D texture;
        Color color;
        public Location location { get; set; }

        public Vector2 velocity { get; set; }
        public float maxSpeed { get; set; }
        public bool isSimulation { get; private set; }
        public Shooter shotBy { get; private set; }

        public Ball(Game game, Shooter shotBy, Location startingLocation, float angle, bool isSimulation = false) : base (game)
        {
            this.shotBy = shotBy;
            location = startingLocation;
            color = RandomHelper.randomColor();

            if (texture == null)
            {
                DrawHelper drawHelper = DrawHelper.getInstance();
                texture = drawHelper.circleTexture;
            }

            velocity = new PolarCoordinate(PhysicsSettings.MAX_BALL_SPEED, angle).toCartesian();

            maxSpeed = PhysicsSettings.MAX_BALL_SPEED;

            this.isSimulation = isSimulation;
        }

        public override void Update(GameTime gameTime)
        {
            if (ballFallen() && !isSimulation)
            {
                EventHandlers.raiseEvent(new BallFallenArgs(this));
            }
        }

        public bool ballFallen()
        {
            return Game1.graphics.GraphicsDevice.Viewport.Bounds.Bottom < location.topLeft.toPoint().Y ? true : false;
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
