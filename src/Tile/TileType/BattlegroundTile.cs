using Godot;

namespace royalgameofur
{
    public class BattlegroundTile : TileType
    {
        public override void _Ready()
        {
            Texture = GD.Load("res://textures/tiles/RoadTile.png") as Texture;
            MaxCapacity = 1;
        }
    }
}