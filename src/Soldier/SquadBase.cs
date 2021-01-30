using System.Collections.Generic;
using Godot;

namespace royalgameofur
{
    public class SquadBase : Node2D
    {
        private List<Soldier> QuateredSoldiers = new List<Soldier>();
        private CollisionShape2D button;
        private List<RectangleShape2D> Shapes = new List<RectangleShape2D>();
        private bool Awake { get; set; }
        

        public void _On_Pressed()
        {
            if (Awake)
            {
                SelectSoldier();
            }
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
            SetAreaLength(QuateredSoldiers.Count);
        }

        public void Discharge(Soldier soldier)
        {
            if (!QuateredSoldiers.Exists(each => each == soldier)) return;
            QuateredSoldiers.RemoveAt(QuateredSoldiers.Count - 1);
            SetAreaLength(QuateredSoldiers.Count);
            Sleep();
        }

        private void SetAreaLength(int quateredSoldiersCount)
        {
            HideAllAreas();
            if (quateredSoldiersCount > 0)
                GetNode<Area2D>("SquadBaseArea" + quateredSoldiersCount).Show();
        }

        private void HideAllAreas()
        {
            foreach (var child in GetChildren())
            {
                if (child is Area2D area2D) area2D.Hide();
            }
        }

        public void Wake()
        {
            Awake = true;
            if (QuateredSoldiers == null || QuateredSoldiers.Count == 0) return;
            foreach (var soldier in QuateredSoldiers)
            {
                soldier.Sleep();
            }
        }

        public void Sleep()
        {
            Awake = false;
        }
    }
}