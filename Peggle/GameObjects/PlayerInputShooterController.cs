using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Net;

namespace Peggle
{
    class PlayerInput : IShooterController
    {

        const float MOVEMENT_SPEED = 0.05f;

        readonly static PlayerInput instance = new PlayerInput();

        private PlayerInput()
        { }

        public static PlayerInput getInstance()
        {
            return instance;
        }

        public ShooterInstructions getShooterInstructions(GameTime gameTime, Shooter shooter)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            float shooterMovement = 0.0f;

            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                shooterMovement += MOVEMENT_SPEED;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                shooterMovement -= MOVEMENT_SPEED;
            }

            if (currentKeyboardState.IsKeyDown(Keys.S))
            {
                shooterMovement /= 5.0f;
            }

            bool fireBall;
            if (currentKeyboardState.IsKeyDown(Keys.Space))
            {
                fireBall = true;
            }
            else
            {
                fireBall = false;
            }

            return new ShooterInstructions(shooterMovement, fireBall);
        }
    }
}
