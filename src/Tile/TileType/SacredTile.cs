using Godot;

namespace royalgameofur
{
    public class SacredTile : TileType
    {
        public override void _Ready()
        {
            Texture = GD.Load("res://textures/tiles/SacredTile.png") as Texture;
            MaxCapacity = 1;
        }
    }
}