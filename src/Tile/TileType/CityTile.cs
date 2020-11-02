using Godot;

namespace royalgameofur
{
    public class CityTile : TileStrategy
    {
        
        public override void _Ready()
        {
            base._Ready();
            Texture = GD.Load("res://textures/tiles/CityTile.png") as Texture;
            MaxCapacity = 4;
            Description = "You can rest here! Great cities of Mesopotamia are peaceful places of commerce " +
                          "and no one will risk destroying this peace. " +
                          "But heed this warning: if you rest too long, newcomers will gain control of all the exits " +
                          "and block you in this city.";
        }

        public override bool CanReceiveSoldier(Soldier soldier)
        {
            return FilledCapacity < MaxCapacity;
        }
        
        public override void Receive(Soldier soldier)
        {
            if (FilledCapacity != 0) stationedSoldiers[stationedSoldiers.Count - 1].blocked = true;
            base.Receive(soldier);
            soldier.EndTurn();
        }

        public override void Discharge(Soldier soldier)
        {
            base.Discharge(soldier);
            if (FilledCapacity != 0) stationedSoldiers[FilledCapacity - 1].blocked = false;
        }
    }
}