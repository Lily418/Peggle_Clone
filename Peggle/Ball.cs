using Microsoft.Xna.Framework;
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

        public Ball(Shooter shotBy, Circle startingLocation, float angle, bool isSimulation = false) : base (Game1.game)
        {
            this.shotBy = shotBy;
            this.isSimulation = isSimulation;
            this.location = startingLocation;

            maxSpeed = PhysicsSettings.MAX_BALL_SPEED;
            color = RandomHelper.randomBasicColor();
            velocity = new PolarCoordinate(PhysicsSettings.MAX_BALL_SPEED, angle).toCartesian();
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
            return Game1.graphics.GraphicsDevice.Viewport.Bounds.Bottom < location.top.Y;
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
