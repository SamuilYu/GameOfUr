using Godot;

namespace royalgameofur
{
    public class RoadTile : TileStrategy
    {
        public override void _Ready()
        {
            base._Ready();
            Texture = GD.Load("res://textures/tiles/RoadTile.png") as Texture;
            MaxCapacity = 1;
            Description =
                "You can have a rest by the side of this road, but this place can not house more than one unit. " +
                "And stay on your guard, an enemy can attack from behind";
        }

        public override void Receive(Soldier soldier)
        {
            MayBeAttack(soldier);
            base.Receive(soldier);
            soldier.EndTurn();
        }
    }
}