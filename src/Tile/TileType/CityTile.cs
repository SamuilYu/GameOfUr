using Godot;

namespace royalgameofur
{
    public class CityTile : TileStrategy
    {
        
        public override void _Ready()
        {
            base._Ready();
            Texture = GD.Load("res://textures/tiles/CityTile.png") as Texture;
            MaxCapacity = 4;
        }

        public override bool CanReceiveSoldier(Soldier soldier)
        {
            return FilledCapacity < MaxCapacity;
        }
    }
}