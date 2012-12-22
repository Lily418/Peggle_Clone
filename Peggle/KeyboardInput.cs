using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Peggle
{
    class KeyboardInput : GameComponent
    {
        public KeyboardButtons buttonStates { get; private set; }
        static KeyboardInput keyboardInput;
        KeyboardState previousState = Keyboard.GetState();
        KeyboardState currentState;

        private KeyboardInput() : base(Game1.game)
        {
            Update();
        }

        public static KeyboardInput getInstance()
        {
            if (keyboardInput == null)
            {
                keyboardInput = new KeyboardInput();
            }

            return keyboardInput;
        }

        public override void Update(GameTime gameTime = null)
        {
            KeyboardButtons keyboardButtons = new KeyboardButtons();

            currentState = Keyboard.GetState();

            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                keyboardButtons.addKeyPress(key, checkKey(key));
            }

            previousState = currentState;

            buttonStates = keyboardButtons;
        }

        private KeyboardActions checkKey(Keys key)
        {
            KeyboardActions action;

            if (previousState.IsKeyUp(key))
            {
                if (currentState.IsKeyDown(key))
                {
                    action = KeyboardActions.Pressed;
                }
                else
                {
                    action = KeyboardActions.Unpressed;
                }
            }
            else
            {
                if (currentState.IsKeyDown(key))
                {
                    action = KeyboardActions.Held;
                }
                else
                {
                    action = KeyboardActions.Unpressed;
                }
            }

            
            return action;
        }

        public enum KeyboardActions
        {
            Unpressed, Pressed, Held
        }

        public class KeyboardButtons
        {
            public Dictionary<Keys, KeyboardActions> keyPresses { private set; get; }


            public KeyboardButtons()
            {
                keyPresses = new Dictionary<Keys, KeyboardActions>();
            }

            public void addKeyPress(Keys key, KeyboardActions action)
            {
                keyPresses.Add(key, action);
            }
        }

    }
}
