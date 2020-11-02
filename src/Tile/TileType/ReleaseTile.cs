namespace royalgameofur
{
    public class ReleaseTile : TileStrategy
    {
        public override void _Ready()
        {
            FilledCapacity = 0;
            MaxCapacity = 1;
            Description =
                "This is the end of the journey. When you reach it, your brothers-at-arms " +
                "will salute your heroism and dream of reaching this place as well.";
        }

        public override void Receive(Soldier soldier)
        {
            soldier.CurrentTile?.strategy.Discharge(soldier);
            soldier.EndTurn();
            soldier.GetParent().RemoveChild(soldier);
            soldier.QueueFree();
        }

        public override void Discharge(Soldier soldier)
        {
            return;
        }
    }
}