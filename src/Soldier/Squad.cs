using System.Collections.Generic;
using Godot;

namespace royalgameofur
{
    public class Squad : Node2D
    {
        private Soldier ActiveSoldier;
        private SquadPath Path = new SquadPath();
        
        [Export()]
        public PlayerTeam Team { get; private set; }
        
        [Export()]
        public int Steps { get; private set; }

        public override void _Ready()
        {
            Path.SetPath(this);
            foreach (Node2D child in GetChildren())
            {
                if (!(child is Soldier soldier)) continue;
                var button = child.GetNode<Button>("SoldierButton");
                button.MouseFilter = Control.MouseFilterEnum.Stop;
            }
            _On_Dice_Roll(Steps);
        }

        public void _On_Dice_Roll(int steps)
        {
            foreach (var child in GetChildren())
            {
                if (!(child is Soldier soldier)) continue;
                List<KeyValuePair<Tile, SoldierTenure>> nextTiles = Path.GetNext(soldier.CurrentTile, soldier.Tenure, steps);
                soldier.March = nextTiles;
                soldier.Wake();
                GetNode<SquadBase>("SquadBase").Wake();
            }
        }

        private void _On_SoldierPressed(Soldier pressedSoldier)
        {
            foreach (var child in GetChildren())
            {
                if(child is Soldier soldier) soldier.Unselect();
            }
            ActiveSoldier = pressedSoldier;
            ActiveSoldier.ZIndex = 6;
            if (ActiveSoldier.March == null) return;
            Tile destination = ActiveSoldier.March[ActiveSoldier.March.Count - 1].Key;
            destination.CheckPlaceForSoldier(ActiveSoldier);
        }
    }
}