using Godot;

namespace royalgameofur
{
    public class TempleTile : TileStrategy
    {
        public override void _Ready()
        {
            base._Ready();
            Texture = GD.Load("res://textures/tiles/TempleTile.png") as Texture;
            MaxCapacity = 1;
        }
    }
}