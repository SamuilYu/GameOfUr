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
        private Button button;
        private int totalSum = 0;
        private int rolling = 0;

        public override void _Ready()
        {
            button = GetNode<Button>("DiceButton");
            button.Connect("pressed", this, "Roll");
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
            button.Flat = false;
            button.MouseFilter = Control.MouseFilterEnum.Stop;
            GD.Print(currentTurn);
        }

        private void ReRoll()
        {
            button.Flat = false;
            button.MouseFilter = Control.MouseFilterEnum.Stop;
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
            button.Flat = true;
            button.MouseFilter = Control.MouseFilterEnum.Ignore;
        }

        private void RollStopped(TwoSidedDie die)
        {
            totalSum += die.Side == DiceSide.Up ? 1 : 0;
            rolling--;
            if (rolling != 0) return;
            if (totalSum == 0) totalSum = 4;
            EmitSignal(currentTurn + "Roll", totalSum);
        }

    }
}