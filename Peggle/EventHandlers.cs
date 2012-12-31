using System;

namespace Peggle
{
    class EventHandlers
    {
        private static EventHandlers instance;

        public EventHandler<CollisionArgs>  collision;
        public EventHandler<BallFallenArgs> ballFallen;
        public EventHandler<LevelResetRequestArgs> levelResetRequest;
        public EventHandler<TurnChangeArgs> turnChange;

        public static EventHandlers getInstance()
        {
            return instance ?? (instance = new EventHandlers());
        }

        public static void resetEventHandlers()
        {
            instance = new EventHandlers();
        }

        private EventHandlers()
        {
        }

        public void raiseEvent(EventArgs e)
        {
            if (e is CollisionArgs)
            {
                if (collision != null)
                {
                    collision("EventHandler", (CollisionArgs)e);
                }
            }
            else if (e is BallFallenArgs)
            {
                if (ballFallen != null)
                {
                    ballFallen("EventHandler", (BallFallenArgs)e);
                }
            }
            else if (e is LevelResetRequestArgs)
            {
                if (levelResetRequest != null)
                {
                    levelResetRequest("EventHandler", (LevelResetRequestArgs)e);
                }
            }
            else if (e is TurnChangeArgs)
            {
                if (turnChange != null)
                {
                    turnChange("EventHandler", (TurnChangeArgs)e);
                }
            }
            else
            {
                throw new ArgumentException("EventHandler can only raise events of recognised types");
            }


        }


    }

    public class BallFallenArgs : EventArgs
    {
        public Ball ball { private set; get; }

        public BallFallenArgs(Ball ball)
        {
            this.ball = ball;
        }

    }

    public class LevelResetRequestArgs : EventArgs
    {
        public EndRoundAction action;

        public LevelResetRequestArgs(EndRoundAction action)
        {
            this.action = action;
        }
    }

    public enum EndRoundAction
    {
        Reset, Menu
    }

    public class TurnChangeArgs : EventArgs
    {
        public Shooter activatedShooter;
        public Shooter deactivatedShooter;

        public TurnChangeArgs(Shooter activatedShooter, Shooter deactivatedShooter)
        {
            this.activatedShooter = activatedShooter;
            this.deactivatedShooter = deactivatedShooter;
        }
    }



    public class CollisionArgs : EventArgs
    {
        public IEntityPhysics collidingObject { private set; get; }
        
        //These variables are kept seperatly as they are not part of Entity interface, they are 'Made Up' by the collision detection system to deal with walls
        public float hitObjectAngle { private set; get; }
        public IEntity hitObject { private set; get; }
        
        public float penetration { private set; get; }

        public CollisionArgs(IEntityPhysics collidingObject, IEntity hitObject, float hitAngle, float penetration)
        {
            this.hitObject = hitObject;
            this.collidingObject = collidingObject;
            this.hitObjectAngle = hitAngle;
            this.penetration = penetration;
        }
    }
}
