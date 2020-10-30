using Godot;

namespace royalgameofur
{
    public class CityTile : TileType
    {
        public override void _Ready()
        {
            Texture = GD.Load("res://textures/tiles/CityTile.png") as Texture;
            MaxCapacity = 4;
        }
    }
}