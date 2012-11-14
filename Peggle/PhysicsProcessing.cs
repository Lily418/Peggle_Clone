using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Peggle
{
    class PhysicsProcessing : GameComponent
    {
        Vector2 gravity = new Vector2(0, 0.005f);

        public PhysicsProcessing(Game game)
            : base(game)
        {
            

        }

        public override void Update(GameTime gameTime)
        {
            CollisionDetection.checkCollisions();

            foreach (IEntityPhysics moveableEntity in Game1.entities.OfType<IEntityPhysics>())
            {   


                //Gravity
                moveableEntity.velocity = Vector2.Add(moveableEntity.velocity, gravity);
                
                //Limit max speed
                moveableEntity.velocity = moveableEntity.velocity.shorten(moveableEntity.maxSpeed);

                moveableEntity.location.topLeft += moveableEntity.velocity;

               
            }

            


        }

    }
}
