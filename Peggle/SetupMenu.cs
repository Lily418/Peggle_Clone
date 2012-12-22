using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Helper;
using System.Diagnostics;

namespace Peggle
{
    class SetupMenu : DrawableGameComponent
    {
        int selectedShooterIndex = 0;
        List<KeyValuePair<String, Type>> shooterTypes = new List<KeyValuePair<String, Type>>();
        List<Shooter> shooters = new List<Shooter>();

        int selectedColorIndex = 0;
        List<Color> colors = new List<Color>();

        public SetupMenu()
            : base(Game1.game)
        {
            shooterTypes.Add(new KeyValuePair<string, Type>("Local Player", typeof(PlayerInput)));
            shooterTypes.Add(new KeyValuePair<string, Type>("AI",           typeof(AI)));
            shooterTypes.Add(new KeyValuePair<string, Type>("Start Game",   null));

            

            colors.Add(Color.Red);
            colors.Add(Color.Blue);
            colors.Add(Color.Green);
            colors.Add(Color.Yellow);
            colors.Add(Color.Purple);
            colors.Add(Color.Orange);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardInput.KeyboardButtons keyboardButtons = KeyboardInput.getInstance().buttonStates;

            if (keyboardButtons.keyPresses[Keys.Down] == KeyboardInput.KeyboardActions.Pressed)
            {
                selectedShooterIndex++;
            }
            else if (keyboardButtons.keyPresses[Keys.Up] == KeyboardInput.KeyboardActions.Pressed)
            {
                selectedShooterIndex--;
            }

            if (keyboardButtons.keyPresses[Keys.Left]      == KeyboardInput.KeyboardActions.Pressed)
            {
                selectedColorIndex--;
            }
            else if (keyboardButtons.keyPresses[Keys.Right] == KeyboardInput.KeyboardActions.Pressed)
            {
                selectedColorIndex++;
            }

            selectedShooterIndex = (int)MathHelper.Clamp(selectedShooterIndex, 0, shooterTypes.Count - 1);
            selectedColorIndex   = (int)MathHelper.Clamp(selectedColorIndex  , 0, colors.Count - 1);

            if (keyboardButtons.keyPresses[Keys.Enter] == KeyboardInput.KeyboardActions.Pressed)
            {
                Type typeToCreate = shooterTypes[selectedShooterIndex].Value;

                if (typeToCreate == null)
                {
                    Game1.setLevelManager(new LevelStateManager(shooters));
                    Game1.levelStateManager.loadLevel();
                }
                else if (typeToCreate == typeof(PlayerInput))
                {
                    shooters.Add(new Shooter(colors[selectedColorIndex], PlayerInput.getInstance(), "Player " + (shooters.Count + 1)));
                }
                else if (typeToCreate == typeof(AI))
                {
                    shooters.Add(new Shooter(colors[selectedColorIndex], new AI(), "AI " + (shooters.Count + 1)));
                }
                
            }
        }

        public override void Draw(GameTime gameTime)
        {
            DrawHelper dh = DrawHelper.getInstance();


            dh.sb.Begin();
            Rectangle viewport = Game1.graphics.GraphicsDevice.Viewport.Bounds;
            
            float y = 50;
            int index = 0;
            foreach (KeyValuePair<String, Type> shooterType in shooterTypes)
            {
                float x = (viewport.Width / 2) - (dh.font.MeasureString(shooterType.Key).X / 2);

                Color drawColor = index == selectedShooterIndex ? Color.Red : Color.White;

                dh.sb.DrawString(dh.font, shooterType.Key, new Vector2(x, y), drawColor); 

                y += dh.font.MeasureString(shooterType.Key).Y;
                index++;
            }


            const int COLORBLOB_DIAMETER = 15;
            const int COLORBLOB_SPACING = 10;
            int colorY = 500;
            int colorX = viewport.Center.X - ((COLORBLOB_DIAMETER * colors.Count) / 2) - ((COLORBLOB_SPACING * colors.Count) / 2);

            index = 0;
            foreach (Color color in colors)
            {
                Rectangle drawPosition = new Rectangle(colorX, colorY, COLORBLOB_DIAMETER, COLORBLOB_DIAMETER);
                dh.sb.Draw(dh.circleTexture, drawPosition, color);
                colorX += COLORBLOB_DIAMETER + COLORBLOB_SPACING;

                if (index == selectedColorIndex)
                {
                    drawPosition.Inflate(4, 4);

                    Rectangle[] lines = {new Rectangle(drawPosition.Left - 1,     drawPosition.Y,          drawPosition.Width + 1,  1),
                                         new Rectangle(drawPosition.Left - 1,     drawPosition.Y,          1,                       drawPosition.Height),
                                         new Rectangle(drawPosition.Right,        drawPosition.Y,          1,                       drawPosition.Height),
                                         new Rectangle(drawPosition.Left,         drawPosition.Bottom - 1, drawPosition.Width,      1)
                                        };

                    foreach (Rectangle line in lines)
                    {
                        dh.sb.Draw(dh.dummyTexture, line, Color.Crimson);
                    }

                }

                index++;
            }

            int playerIndex = 1;
            float drawPositionY = 150;

            foreach (Shooter shooter in shooters)
            {
                String nameLabel = shooter.shooterName;

                float drawPositionX = (viewport.Width / 2) - (dh.font.MeasureString(nameLabel).X / 2);
                dh.sb.DrawString(dh.font, nameLabel, new Vector2(drawPositionX, drawPositionY), shooter.color);
                playerIndex++;
                drawPositionY += dh.font.MeasureString(nameLabel).Y;
            }

            dh.sb.End();
        }
    }
}
