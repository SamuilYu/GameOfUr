using Godot;

namespace royalgameofur
{
    public class SacredTile : TileStrategy
    {
        public override void _Ready()
        {
            base._Ready();
            Texture = GD.Load("res://textures/tiles/SacredTile.png") as Texture;
            MaxCapacity = 1;
            Description = "For a novice this is a place just like any other. " +
                          "But veterans who have come this far and rest assured - they are safe. " +
                          "Their skill is recognized and respected through out the land";
        }

        public override bool CanReceiveSoldier(Soldier soldier)
        {
            if (FilledCapacity == MaxCapacity && stationedSoldiers[FilledCapacity - 1].Tenure == SoldierTenure.Veteran) return false;
            return base.CanReceiveSoldier(soldier);
        }

        public override void Receive(Soldier soldier)
        {
            MayBeAttack(soldier);
            base.Receive(soldier);
            soldier.EndTurn();        
        }
    }
}