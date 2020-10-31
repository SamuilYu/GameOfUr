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
        }

        public override bool CanReceiveSoldier(Soldier soldier)
        {
            return FilledCapacity < MaxCapacity && (CurrentTeam == soldier.Team || CurrentTeam == PlayerTeam.None);
        }
    }
}
