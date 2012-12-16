using System.Linq;
using Microsoft.Xna.Framework;

namespace Peggle
{
    class LevelStateManager
    {

        public Level currentLevel { private set; get; }
        
        public LevelStateManager()
        {
            EventHandlers.ballFallen += ballFallenEventHandler;
            EventHandlers.levelResetRequest += levelResetRequestHander;
            currentLevel = LevelLoader.loadXML(@"Content\level.xml");
            currentLevel.load();
        }

        public void ballFallenEventHandler(object sender, BallFallenArgs e)
        {
            


        }

        public void levelResetRequestHander(object sender, LevelResetRequestArgs e)
        {
            currentLevel.load();
        }

        private void endLevel()
        {
            foreach (GameComponent gameC in Game1.getComponents())
            {
                gameC.Enabled = false;
            }

            Game1.addGameComponent(new EndLevelOverlay());
            
        }
    }
}
