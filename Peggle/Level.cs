using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Peggle
{
    class Level
    {
        Game1 game;

        List<CircularTarget> circularTargets = new List<CircularTarget>();

        public Level(Game1 game)
        {
            this.game = game;
        }

        public void addElement(object element)
        {
            if (element is CircularTarget)
            {
                circularTargets.Add((CircularTarget)element);
            }
            else
            {
                throw new ElementUnknownException(element.GetType() + " was not recognised as a level element");
            }
        }

        //Should add the level to the entity lists and set up the components
        public void load()
        {
            game.clearGameComponents();
            game.addGameComponent(new Shooter(game, Color.Red, new Rectangle(360, 0, 80, 20), PlayerInput.getInstance()));
            game.addGameComponent(new PhysicsProcessing(game));
            game.addGameComponent(new CollisionResolver(game));
            addComponentList(circularTargets);
        }

        private void addComponentList(IEnumerable<GameComponent> gameComponents)
        {
            foreach (GameComponent gc in gameComponents)
            {
                game.addGameComponent(gc);
            }
        }

        class ElementUnknownException : Exception
        {
            public ElementUnknownException(String message) : base(message) { }
        }
    }
}
