using System.Collections.Generic;
using Godot;

namespace royalgameofur
{
    public class SquadBase : Node2D
    {
        private List<Soldier> QuateredSoldiers = new List<Soldier>();

        public void Receive(Soldier soldier)
        {
            var soldierPosition = Position;
            soldierPosition.x -= QuateredSoldiers.Count * 90;
            soldier.Position = soldierPosition;
            QuateredSoldiers.Add(soldier);
        }

        public void Discharge()
        {
            QuateredSoldiers.RemoveAt(QuateredSoldiers.Count - 1);
        }
    }
}