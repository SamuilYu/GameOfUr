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

        public override void Receive(Soldier soldier)
        {
            MayBeAttack(soldier);
            base.Receive(soldier);
            soldier.EndTurn();
        }

    }
}