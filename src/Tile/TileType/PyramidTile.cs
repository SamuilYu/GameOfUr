using Godot;

namespace royalgameofur
{
    public class PyramidTile : TileStrategy
    {
        public override void _Ready()
        {
            base._Ready();
            Texture = GD.Load("res://textures/tiles/PyramidTile.png") as Texture;
            MaxCapacity = 1;
        }
    }
}