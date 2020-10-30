using Godot;

namespace royalgameofur
{
    public class PyramidTile : TileType
    {
        public override void _Ready()
        {
            Texture = GD.Load("res://textures/tiles/PyramidTile.png") as Texture;
            MaxCapacity = 1;
        }
    }
}