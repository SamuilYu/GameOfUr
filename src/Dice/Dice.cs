using System;
using Godot;

namespace royalgameofur
{
    public class Dice : Node2D
    {
        [Signal]
        delegate void WhiteRoll(int steps); 
        
        [Signal]
        delegate void BlackRoll(int steps);

        public PlayerTeam currentTurn = PlayerTeam.None;
        private int totalSum = 0;
        private int rolling = 0;
        private bool active;

        public override void _Ready()
        {
            NewTurn();
        }

        private void NewTurn()
        {
            switch (currentTurn)
            {
                case PlayerTeam.None:
                    currentTurn = PlayerTeam.Black;
                    break;
                case PlayerTeam.White:
                    currentTurn = PlayerTeam.Black;
                    break;
                case PlayerTeam.Black:
                    currentTurn = PlayerTeam.White;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            active = true;
            GD.Print(currentTurn);
        }

        private void ReRoll()
        {
            active = true;
            GD.Print(currentTurn);
        }

        private void Roll()
        {
            totalSum = 0;
            rolling = 0;
            foreach (var child in GetChildren())
            {
                if (child is TwoSidedDie die)
                {
                    rolling++;
                    die.Roll();
                }
            }

            active = false;
        }

        private void RollStopped(TwoSidedDie die)
        {
            totalSum += die.Side == DiceSide.Up ? 1 : 0;
            rolling--;
            if (rolling != 0) return;
            if (totalSum == 0) totalSum = 4;
            EmitSignal(currentTurn + "Roll", totalSum);
        }

        public void Pressed()
        {
            if (active)
            {
                Roll();
            }
        }
    }
}