using Godot;

namespace royalgameofur
{
    public class BattlegroundTile : TileStrategy
    {
        public override void _Ready()
        {
            base._Ready();
            Texture = GD.Load("res://textures/tiles/RoadTile.png") as Texture;
            MaxCapacity = 1;
        }
    }
}