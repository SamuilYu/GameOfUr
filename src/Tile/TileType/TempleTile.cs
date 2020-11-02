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
            Description = "This is a beautiful temple! " +
                          "Hymns and prayers inspire soldiers to keep on marching. " +
                          "You can roll the dice of fate again and make one more move. " +
                          "Yet the sanctity of this place does not protect you against an attack.";
        }

        public override void Receive(Soldier soldier)
        {
            MayBeAttack(soldier);
            base.Receive(soldier);
            soldier.GetParent<Squad>().MakeReRoll();
        }
    }
}