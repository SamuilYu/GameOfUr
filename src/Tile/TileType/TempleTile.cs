using Godot;

namespace royalgameofur
{
    public class TempleTile : TileType
    {
        public override void _Ready()
        {
            Texture = GD.Load("res://textures/tiles/TempleTile.png") as Texture;
            MaxCapacity = 1;
        }
    }
}