using System.Collections.Generic;
using Godot;

namespace royalgameofur
{
    public class SquadBase : Node2D
    {
        private List<Soldier> QuateredSoldiers = new List<Soldier>();
        private Button button;

        public override void _Ready()
        {
            button = GetNode<Button>("BaseButton");
            button.Connect("pressed", this, "SelectSoldier");
            button.Flat = true;
            button.MouseFilter = Control.MouseFilterEnum.Ignore;
        }

        private void SelectSoldier()
        {
            if (QuateredSoldiers != null && QuateredSoldiers.Count != 0)
            {
                foreach (var child in GetParent().GetChildren())
                {
                    if (child is Soldier soldier) soldier.Unselect();
                }
                QuateredSoldiers[QuateredSoldiers.Count - 1].Select();
            }
        }

        public void Receive(Soldier soldier)
        {
            var soldierPosition = Position;
            soldierPosition.x -= QuateredSoldiers.Count * 90;
            soldier.Position = soldierPosition;
            soldier.CurrentTile = null;
            soldier.ChangeTenure(SoldierTenure.Private);
            soldier.ZIndex = 1;
            soldier.PreviousZIndex = 1;
            QuateredSoldiers.Add(soldier);
            button.SetSize(new Vector2(QuateredSoldiers.Count * 90,90));
            if (QuateredSoldiers.Count != 1) button.SetGlobalPosition(button.RectGlobalPosition + new Vector2(-90, 0));
        }

        public void Discharge(Soldier soldier)
        {
            if (!QuateredSoldiers.Exists(each => each == soldier)) return;
            QuateredSoldiers.RemoveAt(QuateredSoldiers.Count - 1);
            button.MouseFilter = Control.MouseFilterEnum.Ignore;
            button.Flat = true;
            if (QuateredSoldiers.Count != 0) button.SetSize(new Vector2(QuateredSoldiers.Count * 90,90));
            if (QuateredSoldiers.Count != 0) button.SetGlobalPosition(button.RectGlobalPosition + new Vector2(90, 0));
        }

        public void Wake()
        {
            if (QuateredSoldiers == null || QuateredSoldiers.Count == 0) return;
            button.MouseFilter = Control.MouseFilterEnum.Stop;
            button.Flat = false;
            foreach (var soldier in QuateredSoldiers)
            {
                soldier.Sleep();
            }
        }

        public void Sleep()
        {
            button.Flat = true;
            button.MouseFilter = Control.MouseFilterEnum.Ignore;
        }
    }
}