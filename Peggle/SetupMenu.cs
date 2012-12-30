using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Helper;
using System.Diagnostics;
using Networking;
using System.Threading;
using System.Net;
using System.Collections.Concurrent;

namespace Peggle
{
    class SetupMenu : DrawableGameComponent
    {
        readonly TimeSpan REQUEST_TIMEOUT = TimeSpan.FromSeconds(5); 

        int selectedShooterIndex = 0;
        List<KeyValuePair<String, MenuOptions>> shooterTypes = new List<KeyValuePair<String, MenuOptions>>();
        List<Shooter> shooters = new List<Shooter>();

        int selectedColorIndex = 0;
        List<Color> colors = new List<Color>();

        public ConcurrentBag<KeyValuePair<System.Net.IPAddress, DateTime>> playerRequests = new ConcurrentBag<KeyValuePair<System.Net.IPAddress, DateTime>>();

        bool startGame = false;

        public SetupMenu()
            : base(Game1.game)
        {
            shooterTypes.Add(new KeyValuePair<string, MenuOptions>("Local Player", MenuOptions.PlayerInput));
            shooterTypes.Add(new KeyValuePair<string, MenuOptions>("AI",           MenuOptions.AI));
            shooterTypes.Add(new KeyValuePair<string, MenuOptions>("Add Network Player", MenuOptions.NetworkPlayer));
            shooterTypes.Add(new KeyValuePair<string, MenuOptions>("Enter Client Mode", MenuOptions.ClientMode));
            shooterTypes.Add(new KeyValuePair<string, MenuOptions>("Start Game",   MenuOptions.StartGame));
            

            colors.Add(Color.Red);
            colors.Add(Color.Blue);
            colors.Add(Color.Green);
            colors.Add(Color.Yellow);
            colors.Add(Color.Purple);
            colors.Add(Color.Orange);

            PacketEvents.playerRequestResponse += playerRequestResponseEventHandler;

        }

        enum MenuOptions
        {
            PlayerInput, AI, ClientMode, StartGame, NetworkPlayer
        }

        public override void Update(GameTime gameTime)
        {
            if (startGame)
            {
                if (playerRequests.Count > 0)
                {
                    foreach (KeyValuePair<System.Net.IPAddress, DateTime> request in playerRequests)
                    {
                        //Debug.WriteLine(playerRequests.Count);
                        if (DateTime.Now - request.Value > REQUEST_TIMEOUT)
                        {
                            Game1.addGameComponent(new Networking.TimeoutInterface(this, request));
                        }
                    }
                }
                else
                {
                    if (shooters.Any())
                    {
                        List<IPAddress> clients = new List<IPAddress>();
                        for (int i = 0; i < shooters.Count; i++)
                        {
                            if (shooters[i].getControllerType() == typeof(NetworkShooter))
                            {
                                NetworkInterface.send(new SetupPacket(shooters, shooters[i]), ((NetworkShooter)shooters[i].shooterController).ipAddress);
                                clients.Add(((NetworkShooter)shooters[i].shooterController).ipAddress);
                            }
                        }

                        foreach (Shooter shooter in shooters)
                        {
                            if (shooter.getControllerType() != typeof(NetworkShooter))
                            {
                                shooter.clients = clients;
                            }
                        }

                        Game1.setLevelManager(new LevelStateManager(shooters));
                        Game1.levelStateManager.loadLevel();
                    }
                    else
                    {
                        new Alert("First add some players", new Vector2(DrawHelper.getInstance().centerX("First add some players"), 200), TimeSpan.FromSeconds(2), Color.Red);
                        startGame = false;
                    }
                }
            }
            else
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

                if (keyboardButtons.keyPresses[Keys.Left] == KeyboardInput.KeyboardActions.Pressed)
                {
                    selectedColorIndex--;
                }
                else if (keyboardButtons.keyPresses[Keys.Right] == KeyboardInput.KeyboardActions.Pressed)
                {
                    selectedColorIndex++;
                }

                selectedShooterIndex = (int)MathHelper.Clamp(selectedShooterIndex, 0, shooterTypes.Count - 1);
                selectedColorIndex = (int)MathHelper.Clamp(selectedColorIndex, 0, colors.Count - 1);

                if (keyboardButtons.keyPresses[Keys.Enter] == KeyboardInput.KeyboardActions.Pressed)
                {
                    switch (shooterTypes[selectedShooterIndex].Value)
                    {
                        case MenuOptions.StartGame:
                            startGame = true;
                            break;

                        case MenuOptions.PlayerInput:
                            shooters.Add(new Shooter(colors[selectedColorIndex], PlayerInput.getInstance(), "Player " + (shooters.Count + 1)));
                            break;

                        case MenuOptions.AI:
                            shooters.Add(new Shooter(colors[selectedColorIndex], new AI(), "AI " + (shooters.Count + 1)));
                            break;
                        case MenuOptions.ClientMode:
                            Game1.removeGameComponent(this);
                            Game1.addGameComponent(new ClientMode());
                            break;

                        case MenuOptions.NetworkPlayer:
                            NetworkPlayerOptions networkPlayerOptions;
                            Game1.addGameComponent(networkPlayerOptions = new NetworkPlayerOptions(this));

                            break;

                    }

                }
            }

        }

        public void playerRequestResponseEventHandler(object sender, PlayerRequestResponseArgs e)
        {
            IEnumerable<KeyValuePair<IPAddress, DateTime>> requests;
            if ((requests = playerRequests.Where(req => req.Key.Equals(e.ip))) != null)
            {
                if (e.answer)
                {
                    Shooter newShooter = new Shooter(Color.Red, null, "Network Player");
                    newShooter.shooterController = new NetworkShooter(e.ip, newShooter.identifier);
                    shooters.Add(newShooter);

                    foreach (KeyValuePair<IPAddress, DateTime> remove in requests)
                    {
                        KeyValuePair<IPAddress, DateTime> copy = remove;
                        playerRequests.TryTake(out copy);
                    }
                }
                else
                {
                    foreach (KeyValuePair<IPAddress, DateTime> remove in requests)
                    {
                        KeyValuePair<IPAddress, DateTime> copy = remove;
                        playerRequests.TryTake(out copy);
                        new Alert(remove.Key + " rejected your request", new Vector2(10, 450), TimeSpan.FromSeconds(2), Color.Red);
                    }
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
            foreach (KeyValuePair<String, MenuOptions> shooterType in shooterTypes)
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

            float drawPositionY = 200;

            foreach (Shooter shooter in shooters)
            {
                String nameLabel = shooter.shooterName;

                float drawPositionX = (viewport.Width / 2) - (dh.font.MeasureString(nameLabel).X / 2);
                dh.sb.DrawString(dh.font, nameLabel, new Vector2(drawPositionX, drawPositionY), shooter.color);
                drawPositionY += dh.font.MeasureString(nameLabel).Y;
            }

            if (playerRequests.Count > 0)
            {
                String waitingString = "Waiting for responses from network players";
                float waitingStringX = (viewport.Width / 2) - (dh.font.MeasureString(waitingString).X / 2f);
                dh.sb.DrawString(dh.font, "Waiting for responses from network players", new Vector2(waitingStringX, 550), Color.White);
            }

            dh.sb.End();
        }
    }
}
