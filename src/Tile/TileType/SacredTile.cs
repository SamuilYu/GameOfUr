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
        }

        public override bool CanReceiveSoldier(Soldier soldier)
        {
            if (FilledCapacity == MaxCapacity && stationedSoldiers[FilledCapacity].Tenure == SoldierTenure.Veteran) return false;
            return base.CanReceiveSoldier(soldier);
        }
    }
}