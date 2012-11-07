using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Peggle
{
    class DrawHelper
    {
        private static DrawHelper instance;
        public Texture2D dummyTexture { get; private set; }
        public Texture2D circleTexture { get; private set; }
        public SpriteBatch sb { get; private set; }

        private DrawHelper()
        {
            dummyTexture = new Texture2D(Game1.graphics.GraphicsDevice, 1, 1);
            dummyTexture.SetData(new[] { Color.White });
            circleTexture = Game1.cm.Load<Texture2D>("circle");

            sb = new SpriteBatch(Game1.graphics.GraphicsDevice);
        }

        public static DrawHelper getInstance()
        {
            if (instance == null)
            {
                instance = new DrawHelper();
            }

            return instance;
        }

        
    }
}
