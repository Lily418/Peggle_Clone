using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Helper;

namespace Peggle
{
    class PhysicsProcessing : GameComponent
    {
        Vector2 gravity = PhysicsSettings.GRAVITY;

        public PhysicsProcessing(Game game)
            : base(game)
        {
        }

        public override void Update(GameTime gameTime)
        {
            //Gravity must be applied before collision check, otherwise gravity will cause entities to move inside other entites.
            foreach (IEntityPhysics moveableEntity in Game1.getComponents().OfType<IEntityPhysics>())
            {
                //Gravity
                moveableEntity.velocity = Vector2.Add(moveableEntity.velocity, gravity);
            }

            CollisionDetection.checkCollisions();

            foreach (IEntityPhysics moveableEntity in Game1.getComponents().OfType<IEntityPhysics>())
            {   
                
                //Limit max speed
                moveableEntity.velocity = moveableEntity.velocity.shorten(moveableEntity.maxSpeed);

                moveableEntity.location.topLeft += moveableEntity.velocity;

               
            }

            

            


        }

    }
}
