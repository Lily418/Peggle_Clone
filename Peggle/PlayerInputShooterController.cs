using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Peggle
{
    class PlayerInput : IShooterController
    {

        static PlayerInput instance = new PlayerInput();

        TimeSpan inputLastUpdated = TimeSpan.Zero;
        ShooterInstructions shooterInstructions = null;

        private PlayerInput()
        {
        }

        public static PlayerInput getInstance()
        {
            return instance;
        }

        public ShooterInstructions getShooterInstructions(TimeSpan currentElapsedTime, Shooter shooter)
        {
            if (shooterInstructions == null || inputLastUpdated != currentElapsedTime)
            {
                KeyboardState currentKeyboardState = Keyboard.GetState();

                float shooterMovement = 0.0f;

                if(currentKeyboardState.IsKeyDown(Keys.Left))
                {
                    shooterMovement += 0.05f;
                }

                if (currentKeyboardState.IsKeyDown(Keys.Right))
                {
                    shooterMovement -= 0.05f;
                }

                if (currentKeyboardState.IsKeyDown(Keys.S))
                {
                    shooterMovement /= 5.0f;
                }

                bool fireBall = false;
                if(currentKeyboardState.IsKeyDown(Keys.Space))
                {
                    fireBall = true;
                }

                shooterInstructions = new ShooterInstructions(shooterMovement, fireBall);
                inputLastUpdated = currentElapsedTime;

                
            }

            return shooterInstructions;
        }


        
    }
    


}
