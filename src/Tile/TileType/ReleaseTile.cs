namespace royalgameofur
{
    public class ReleaseTile : TileStrategy
    {
        public override void _Ready()
        {
            FilledCapacity = 0;
            MaxCapacity = 1;
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