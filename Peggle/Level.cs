﻿using System;
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
        public PhysicsProcessing physicsProcessor { private set; get; }

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

            addComponentList(circularTargets);
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
