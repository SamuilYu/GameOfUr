using Godot;

namespace royalgameofur
{
    public class ReleaseTile : TileStrategy
    {
        public override void _Ready()
        {
            Texture = GD.Load("res://textures/tiles/ReleaseTile.png") as Texture;
            FilledCapacity = 0;
            MaxCapacity = 1;
            Description =
                "This is the end of the journey. When you reach it, your brothers-at-arms " +
                "will salute your heroism and dream of reaching this place as well.";
        }

        public override void Receive(Soldier soldier)
        {
            soldier.CurrentTile?.strategy.Discharge(soldier);
            Squad squad = soldier.GetParent<Squad>();
            squad.Tracker.Increment();
            soldier.GetParent().RemoveChild(soldier);
            squad.EndTurn();
            soldier.QueueFree();
        }

        public override void Discharge(Soldier soldier)
        {
            return;
        }
    }
}