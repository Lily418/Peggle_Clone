using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Helper;

namespace Peggle
{
    class Level
    {
        public PhysicsProcessing physicsProcessor { private set; get; }
        public TurnManager turnManager { private set; get; }
        List<Target> targetCache = new List<Target>();
        public List<Shooter> shooters { private set; get; }
        CollisionResolver collisionResolver;
        

        public Level()
        {
            shooters = new List<Shooter>();

            physicsProcessor  = new PhysicsProcessing();
            collisionResolver = new CollisionResolver();
            
            Shooter playerShooter = new Shooter(Color.Red,     new Rectangle(150, 0, 80, 20), PlayerInput.getInstance());
            Shooter aiShooter     = new Shooter(Color.Green,   new Rectangle(300, 0, 80, 20), new AI());
            shooters.Add(playerShooter);
            //shooters.Add(aiShooter);

        }

        public void addTarget(Target target)
        {
            targetCache.Add(target);
        }

        public void load()
        {
            Game1.clearGameComponents();

            Game1.addGameComponent(physicsProcessor);
            Game1.addGameComponent(collisionResolver);

            Queue<Shooter> shooterQueue = new Queue<Shooter>();

            float scoreX = 0.02f;
            float scoreY = 0.01f;
            int shooterNumber = 1;
            foreach (Shooter shooter in shooters)
            {
                shooterQueue.Enqueue(shooter);
                String labelString = "Player "+ shooterNumber +":";
                Game1.addGameComponent(new Score(shooter, new Vector2(scoreX, scoreY), labelString));

                shooterNumber++;
                scoreY += DrawHelper.getInstance().font.MeasureString(labelString).Y / Game1.graphics.GraphicsDevice.Viewport.Height;
            }


            foreach(Target target in targetCache)
            {
                if(target is CircularTarget)
                {
                   shooters.ForEach(shooter => shooter.addTarget(((CircularTarget)target).clone()));
                }
                else if(target is CurveTarget)
                {
                    shooters.ForEach(shooter => shooter.addTarget(((CurveTarget)target).clone()));
                }
                else
                {
                    throw new ElementUnknownException("Target Type is unknown by the level loader");
                }
                
            }

            Game1.addGameComponent(turnManager = new TurnManager(shooterQueue));

            Game1.addGameComponent(Game1.levelStateManager);

            foreach (GameComponent gc in Game1.getComponents())
            {
                gc.Enabled = true;
            }
        }

        class ElementUnknownException : Exception
        {
            public ElementUnknownException(String message) : base(message) { }
        }
    }
}
