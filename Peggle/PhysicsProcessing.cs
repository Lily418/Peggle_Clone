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
        Vector2 gravity = new Vector2(0, 0.1f);

        public PhysicsProcessing(Game game)
            : base(game)
        {
        }

        public override void Update(GameTime gameTime)
        {
            

            foreach (IEntityPhysics moveableEntity in Game1.getComponents().OfType<IEntityPhysics>())
            {   


                //Gravity

                moveableEntity.velocity = Vector2.Add(moveableEntity.velocity, gravity);
                
                //Limit max speed
                moveableEntity.velocity = moveableEntity.velocity.shorten(moveableEntity.maxSpeed);

                moveableEntity.location.topLeft += moveableEntity.velocity;

               
            }

            CollisionDetection.checkCollisions();

            


        }

    }
}
