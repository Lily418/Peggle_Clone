﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Peggle;

namespace Helper
{
    class DrawHelper
    {
        private static DrawHelper instance;
        public Texture2D dummyTexture  { get; private set; }
        public Texture2D circleTexture { get; private set; }
        public SpriteBatch sb          { get; private set; }
        public SpriteFont font         { get; private set; }

        private DrawHelper()
        {
            dummyTexture = new Texture2D(Game1.graphics.GraphicsDevice, 1, 1);
            dummyTexture.SetData(new[] { Color.White });
            circleTexture = Game1.cm.Load<Texture2D>("circle");
            font = Game1.cm.Load<SpriteFont>("font");

            sb = new SpriteBatch(Game1.graphics.GraphicsDevice);
        }

        public static DrawHelper getInstance()
        {
            return instance ?? (instance = new DrawHelper());
        }

        public float centerX(String str)
        {
            Rectangle viewport = Game1.graphics.GraphicsDevice.Viewport.Bounds;
            return (viewport.Width / 2) - (font.MeasureString(str).X / 2);
        }
    }
}
