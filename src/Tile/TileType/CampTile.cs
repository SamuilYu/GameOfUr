using Godot;

namespace royalgameofur
{
    public class CampTile : TileType
    {
        public override void _Ready()
        {
            Texture = GD.Load("res://textures/tiles/CampTile.png") as Texture;
            MaxCapacity = 4;
        }
    }
}
