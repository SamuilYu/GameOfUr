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
            Description = "This mountain is one step away from the bloodiest battleground." +
                          " Soldiers that reach that place are experienced warriors. " +
                          "You can bide your time in the mountains and launch a surprise attack " +
                          "on an unlucky soldier that just got promoted.";
        }

        public override void Receive(Soldier soldier)
        {
            MayBeAttack(soldier);
            base.Receive(soldier);
            soldier.EndTurn();
        }

    }
}