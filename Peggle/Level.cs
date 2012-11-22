using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Peggle
{
    class Level
    {
        Game1 game;

        public PhysicsProcessing physicsProcessor { private set; get; }
        List<Target> targets = new List<Target>();

        public Level(Game1 game)
        {
            this.game = game;
        }

        public void addElement(GameComponent element)
        {
            if (element is Target)
            {
                targets.Add((Target)element);
            }
            else
            {
                Debug.Assert(false, "Element not recoginsed by level loader " + element.GetType());
            }
        }

        //Should add the level to the entity lists and set up the components
        public void load()
        {
            Game1.clearGameComponents();
            physicsProcessor = new PhysicsProcessing(game);
            Game1.addGameComponent(physicsProcessor);
            Game1.addGameComponent(new CollisionResolver(game));

            Queue<Shooter> shooters = new Queue<Shooter>();
            Shooter playerShooter = new Shooter(game, Color.Red, new Rectangle(300, 0, 80, 20), PlayerInput.getInstance());
            Shooter aiShooter = new Shooter(game, Color.Green, new Rectangle(420, 0, 80, 20), new AI(game));
            shooters.Enqueue(playerShooter);
            shooters.Enqueue(aiShooter);

            Game1.addGameComponent(new TurnManager(game, shooters));

            addComponentList(targets);
        }

        private void addComponentList(IEnumerable<GameComponent> gameComponents)
        {
            foreach (GameComponent gc in gameComponents)
            {
                Game1.addGameComponent(gc);
            }
        }

        class ElementUnknownException : Exception
        {
            public ElementUnknownException(String message) : base(message) { }
        }
    }
}
