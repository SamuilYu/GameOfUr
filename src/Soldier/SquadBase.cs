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
        }

        private void SelectSoldier()
        {
            if (QuateredSoldiers != null && QuateredSoldiers.Count != 0)
            {
                QuateredSoldiers[QuateredSoldiers.Count - 1].Select();
            }
        }

        public void Receive(Soldier soldier)
        {
            var soldierPosition = Position;
            soldierPosition.x -= QuateredSoldiers.Count * 90;
            soldier.Position = soldierPosition;
            QuateredSoldiers.Add(soldier);
            button.MouseFilter = Control.MouseFilterEnum.Stop;
            button.Flat = false;
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
            if (QuateredSoldiers.Count != 1) button.SetGlobalPosition(button.RectGlobalPosition + new Vector2(90, 0));
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
    }
}