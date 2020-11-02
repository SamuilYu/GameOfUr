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
            Description = "Soldiers that come this far have proved their worth and get promoted. " +
                          "Yet, beware! This is a treacherous land between mountains. And an attack from even " +
                          "an inexperienced enemy can be deadly.";
        }
        
        public override void Receive(Soldier soldier)
        {
            MayBeAttack(soldier);
            base.Receive(soldier);
            soldier.EndTurn();
        }
    }
}