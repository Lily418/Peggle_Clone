using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using Helper;

namespace Peggle
{
    class Level
    {
        Game1 game;

        public PhysicsProcessing physicsProcessor { private set; get; }
        List<Target> targetCache = new List<Target>();
        List<Shooter> shooters = new List<Shooter>();
        CollisionResolver collisionResolver;

        public Level(Game1 game)
        {
            this.game = game;
            physicsProcessor  = new PhysicsProcessing(game);
            collisionResolver = new CollisionResolver(game);
            
            //TODO: Move this to the game setup options
            Shooter playerShooter = new Shooter(game, Color.Red, new Rectangle(150, 0, 80, 20), PlayerInput.getInstance());
            Shooter aiShooter = new Shooter(game, Color.Green, new Rectangle(300, 0, 80, 20), new AI(game));
            shooters.Add(playerShooter);
            shooters.Add(aiShooter);

        }

        public void addElement(GameComponent element)
        {
            if (element is Target)
            {
                targetCache.Add((Target)element);
            }
            else
            {
                Debug.Assert(false, "Element not recognised by level loader " + element.GetType());
            }
        }

        //Should add the level to the entity lists and set up the components
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
                Game1.addGameComponent(new Score(game, shooter, new Vector2(scoreX, scoreY), labelString));

                shooterNumber++;
                scoreY += DrawHelper.getInstance().font.MeasureString(labelString).Y / Game1.graphics.GraphicsDevice.Viewport.Height;
            }

            Game1.addGameComponent(new TurnManager(game, shooterQueue));

            List<Target>targets = new List<Target>();

            foreach(Target target in targetCache)
            {
                if(target is CircularTarget)
                {
                    Game1.addGameComponent(((CircularTarget)target).clone());
                }
                else if(target is CurveTarget)
                {
                    Game1.addGameComponent(((CurveTarget)target).clone());
                }
                else
                {
                    throw new ElementUnknownException("Target Type is unknown by the level loader");
                }
                
            }

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
