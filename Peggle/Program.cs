using System;
using System.Diagnostics;

namespace Peggle
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            //Debugger.Launch();
            using (Game1 game = new Game1())
                game.Run();
        }
    }
#endif
}

