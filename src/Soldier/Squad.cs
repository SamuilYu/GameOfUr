using System.Collections.Generic;
using System.Xml;
using Godot;

namespace royalgameofur
{
    public class Squad : Node2D
    {
        [Signal]
        delegate void ReRoll();
        
        private Soldier ActiveSoldier;
        private SquadPath Path = new SquadPath();
        
        [Export()]
        public PlayerTeam Team { get; private set; }
        
        [Export()]
        public int Steps { get; private set; }

        public override void _Ready()
        {
            Path.SetPath(this);
            var diceSkipButton = GetParent().GetParent().GetNode<Dice>("3D2 Dice").GetNode<Button>("SkipButton");
            GetNode<Button>("SkipButton").SetGlobalPosition(diceSkipButton.GetGlobalRect().Position);
            GetNode<Button>("SkipButton").Hide();
        }

        public void _On_Dice_Roll(int steps)
        {
            var shouldSkip = true;
            foreach (var child in GetChildren())
            {
                if (!(child is Soldier soldier)) continue;
                List<KeyValuePair<Tile, SoldierTenure>> nextTiles = Path.GetNext(soldier.CurrentTile, soldier.Tenure, steps);
                soldier.March = nextTiles;
                if (nextTiles != null && nextTiles[nextTiles.Count - 1].Key.CheckPlaceForSoldier(soldier, false))
                    shouldSkip = false;
                soldier.Wake();
            }
            GetNode<SquadBase>("SquadBase").Wake();
            if (shouldSkip)
            {
                GetNode<Button>("SkipButton").Show();
            }
        }

        public void _On_SoldierPressed(Soldier pressedSoldier)
        {
            foreach (var child in GetChildren())
            {
                if (child is Soldier soldier && soldier != pressedSoldier) soldier.Unselect();
            }
            ActiveSoldier = pressedSoldier;
            if (ActiveSoldier.March == null) return;
            Tile destination = ActiveSoldier.March[ActiveSoldier.March.Count - 1].Key;
            destination.CheckPlaceForSoldier(ActiveSoldier);
        }

        public void EndTurn()
        {
            foreach (var child in GetChildren())
            {
                if (child is Soldier soldier) 
                {
                    soldier.Unselect();
                    soldier.Sleep();
                }
                if (child is SquadBase squadBase) squadBase.Sleep();
            }
            GetParent<Player>().EndThisTurn();
            GetNode<Button>("SkipButton").Hide();
        }

        public void MakeReRoll()
        {
            foreach (var child in GetChildren())
            {
                if (child is Soldier soldier)
                {
                    soldier.Unselect();
                    soldier.Sleep();
                }
                if (child is SquadBase squadBase) squadBase.Sleep();
            }
            EmitSignal("ReRoll");
        }
    }
}