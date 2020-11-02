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

        public override void Receive(Soldier soldier)
        {
            MayBeAttack(soldier);
            base.Receive(soldier);
            soldier.GetParent<Squad>().MakeReRoll();
        }
    }
}