using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Peggle
{
    class PlayerInput : GameComponent
    {
        //TODO:Consider use of MultiCast delegates instead of Events 
        Queue<PlayerInputArgs> input = new Queue<PlayerInputArgs>(1);

        public PlayerInput(Game game)
            : base(game)
        {
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }

    public class PlayerInputArgs : EventArgs
    {
        public Vector2 arrowDirection{public get; private set;}

        public PlayerInputArgs(Vector2 arrowDirection)
        {
            this.arrowDirection = arrowDirection;
        }

    }
}
