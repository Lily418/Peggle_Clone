using System.Linq;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Helper;


namespace Peggle
{
    class LevelStateManager : DrawableGameComponent
    {

        public Level currentLevel { private set; get; }
        public int roundsRemaining   { private set; get; }

        const int MAX_ROUNDS = 5;

        public LevelStateManager() : base (Game1.game)
        {
            roundsRemaining = MAX_ROUNDS;
            EventHandlers.ballFallen += ballFallenEventHandler;
            EventHandlers.levelResetRequest += levelResetRequestHander;
            currentLevel = LevelLoader.loadXML(@"Content\level.xml");
        }

        public void ballFallenEventHandler(object sender, BallFallenArgs e)
        {
            int players = currentLevel.turnManager.noOfPlayers();
            int turns = currentLevel.turnManager.turnCount;

            if (turns % players == 0)
            {
                int roundsPlayed = turns / players;
                roundsRemaining = MAX_ROUNDS - roundsPlayed;

                if (roundsPlayed >= MAX_ROUNDS)
                {
                    endLevel();
                }

                foreach (Shooter shooter in currentLevel.shooters)
                {
                    if (shooter.targets.Find(target => !target.hit) == null)
                    {
                        endLevel();
                    }
                }
            }

        }

        public void levelResetRequestHander(object sender, LevelResetRequestArgs e)
        {
            roundsRemaining = MAX_ROUNDS;
            loadLevel();
        }

        public void loadLevel()
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

        public override void Draw(GameTime gameTime)
        {
            DrawHelper dh = DrawHelper.getInstance();
            
            dh.sb.Begin();

            dh.sb.DrawString(dh.font, "Rounds Remaining: " + roundsRemaining, new Vector2(300, 570), Color.White);

            dh.sb.End();
        }
    }
}
