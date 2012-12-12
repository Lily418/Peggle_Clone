using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using Helper;


namespace Peggle
{
    public class Ball : DrawableGameComponent, IEntityPhysics
    {
        private Color color;

        public Vector2 velocity { get; set; }
        public float maxSpeed { get; set; }

        public bool isSimulation { get; private set; }
        public Shooter shotBy { get; private set; }
        public Circle location { get; private set; }

        public Ball(Game game, Shooter shotBy, Circle startingLocation, float angle, bool isSimulation = false) : base (game)
        {
            this.shotBy = shotBy;
            this.isSimulation = isSimulation;
            maxSpeed = PhysicsSettings.MAX_BALL_SPEED;
            this.location = startingLocation;
            color = RandomHelper.randomBasicColor();

            velocity = new PolarCoordinate(3, angle).toCartesian();
            //velocity = new PolarCoordinate(3, -4.302259F).toCartesian();

           

            
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
            KeyboardState ks = Keyboard.GetState();
            return Game1.graphics.GraphicsDevice.Viewport.Bounds.Bottom < location.top.Y || ks.IsKeyDown(Keys.D);
        }

        public override void Draw(GameTime gameTime)
        {
            location.draw(color);
        }



        public Shape boundingBox()
        {
            return location;
        }
    }
}
