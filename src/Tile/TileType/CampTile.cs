using Godot;

namespace royalgameofur
{
    public class CampTile : TileStrategy
    {
        private PlayerTeam CurrentTeam;
        
        public override void _Ready()
        {
            base._Ready();
            Texture = GD.Load("res://textures/tiles/CampTile.png") as Texture;
            MaxCapacity = 4;
            CurrentTeam = PlayerTeam.None;
            Description = "This is an army camp. Army camps can house up to four units at once." +
                          " Once an army captures a camp, a soldier of an opposing army can not step into this territory until " +
                          "the last soldier leaves.";
        }

        public override bool CanReceiveSoldier(Soldier soldier)
        {
            return FilledCapacity < MaxCapacity && (CurrentTeam == soldier.Team || CurrentTeam == PlayerTeam.None);
        }

        public override void Receive(Soldier soldier)
        {
            CurrentTeam = soldier.Team;
            if (FilledCapacity != 0) stationedSoldiers[stationedSoldiers.Count - 1].blocked = true;
            base.Receive(soldier);
            soldier.EndTurn();
        }
        
        public override void Discharge(Soldier soldier)
        {
            base.Discharge(soldier);
            if (FilledCapacity != 0) stationedSoldiers[FilledCapacity - 1].blocked = false;
            if (FilledCapacity == 0) CurrentTeam = PlayerTeam.None;
        }
    }
}
