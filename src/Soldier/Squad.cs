using System.Collections.Generic;
using System.Xml;
using Godot;

namespace royalgameofur
{
    public class Squad : Node2D
    {
        [Signal]
        delegate void ReRoll();
        
        [Signal]
        public delegate void EndThisTurn();
        
        [Signal]
        public delegate void Win(string name);
        
        private SquadPath Path = new SquadPath();
        
        [Export()]
        public PlayerTeam Team { get; private set; }
        
        [Export()]
        public int Steps { get; private set; }

        public Tracker Tracker { get; set; }

        public override void _Ready()
        {
            Path.SetPath(this);
            InitSkipButton();
            Tracker = GetNode<Tracker>("Tracker");
        }

        private void InitSkipButton()
        {
            var diceSkipButton = GetParent().GetParent().GetNode<Dice>("3D2 Dice").GetNode<Button>("SkipButton");
            GetNode<Button>("SkipButton").SetGlobalPosition(diceSkipButton.GetGlobalRect().Position);
            GetNode<Button>("SkipButton").Hide();
        }

        public void _On_Dice_Roll(int steps)
        {
            var shouldSkip = true;
            foreach (var child in GetChildren())
            {
                // wake every soldier
                if (!(child is Soldier soldier)) continue;
                soldier.Wake();
                
                // calculate march for every soldier and show skip button if appropriate
                List<KeyValuePair<Tile, SoldierTenure>> nextTiles = Path.GetNext(soldier.CurrentTile, soldier.Tenure, steps);
                soldier.March = nextTiles;
                if (nextTiles != null && nextTiles[nextTiles.Count - 1].Key.CheckPlaceForSoldier(soldier, false) && !soldier.blocked)
                    shouldSkip = false;
            }
            GetNode<SquadBase>("SquadBase").Wake();
            if (shouldSkip)
            {
                GetNode<Button>("SkipButton").Show();
            }
        }

        public void WhenSoldierPressedDeselectOthers(Soldier pressedSoldier)
        {
            foreach (var child in GetChildren())
            {
                if (child is Soldier soldier && soldier != pressedSoldier) soldier.Unselect();
            }
        }

        private void DeselectEverything()
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
        }

        public void EndTurn()
        {
            DeselectEverything();
            GetNode<Button>("SkipButton").Hide();
            EmitSignal("EndThisTurn");
        }

        public void MakeReRoll()
        {
            DeselectEverything();
            EmitSignal("ReRoll");
        }

        public override void _PhysicsProcess(float delta)
        {
            if (Tracker.releasedSoldiersCount >= 7)
            {
                EmitSignal("Win", GetParent<Player>().GetPlayerName());
            }
        }
    }
}