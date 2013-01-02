using System.Linq;
using Microsoft.Xna.Framework;
using Helper;

namespace Peggle
{
    public class PhysicsProcessing : GameComponent
    {
        Vector2 gravity = PhysicsSettings.GRAVITY;

        public PhysicsProcessing()
            : base(Game1.game)
        {
        }

        public override void Update(GameTime gameTime)
        {
            const int LOOPS = 2;
            for (int i = 0; i < LOOPS; i++)
            {
                //Gravity must be applied before collision check, otherwise gravity will cause entities to move inside other entites.
                foreach (IEntityPhysics moveableEntity in Game1.getComponents().OfType<IEntityPhysics>())
                {
                    //Gravity
                    moveableEntity.velocity = Vector2.Add(moveableEntity.velocity, gravity / LOOPS);
                }

                CollisionDetection.checkCollisions();

                foreach (IEntityPhysics moveableEntity in Game1.getComponents().OfType<IEntityPhysics>())
                {
                    //Limit max speed
                    moveableEntity.velocity = moveableEntity.velocity.shorten(moveableEntity.maxSpeed);
                    
                    //Move
                    moveableEntity.boundingBox().translate(moveableEntity.velocity / LOOPS);
                }
            }
        }
    }
}
