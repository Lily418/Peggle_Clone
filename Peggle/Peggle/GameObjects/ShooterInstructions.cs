namespace Peggle
{
    public class ShooterInstructions
    {
        public float movementDirection { get; private set; }
        public bool fireBall { get; private set; }

        public ShooterInstructions(float movementDirection, bool fireBall)
        {
            this.movementDirection = movementDirection;
            this.fireBall = fireBall;
        }

    }
}
