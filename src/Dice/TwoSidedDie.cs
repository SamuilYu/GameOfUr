using System;
using System.Drawing;
using Godot;

namespace royalgameofur
{
    public class TwoSidedDie : Node2D
    {
        [Signal]
        delegate void Rolled(TwoSidedDie die);

        private int deltaSteps = 0;
        private const int MeanNumberOfRolls = 6;
        private const int StdDevNumberOfRolls = 4;
        private const double DownSideOccurenceRatio = 0.55;
        private const int OneFlipTime = 300;
        public DiceSide Side { get; set;  }
        private readonly Texture WhiteSideIcon = GD.Load<Texture>("res://textures/dice/WhiteSide.png");
        private readonly Texture BlackSideIcon = GD.Load<Texture>("res://textures/dice/BlackSide.png");
        private int NumberOfRolls = 0;

        public override void _Ready()
        {
            Side = DiceSide.Down;
            Connect("Rolled", GetParent(), "RollStopped");
            SetIcon();
        }

        private void SetIcon()
        {
            switch (Side)
            {
                case DiceSide.Up:
                    this.GetNode<Sprite>("Icon").Texture = WhiteSideIcon;
                    this.GetNode<Sprite>("Icon").Scale = new Vector2((float) 0.24, (float) 0.4);
                    break;
                case DiceSide.Down:
                    this.GetNode<Sprite>("Icon").Texture = BlackSideIcon;
                    this.GetNode<Sprite>("Icon").Scale = new Vector2((float) 0.3, (float) 0.5);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public DiceSide Roll()
        {
            NumberOfRolls = NormalDistributionGenerateInteger(MeanNumberOfRolls, StdDevNumberOfRolls);
            return Side;
        }

        private void MayBeFlip()
        {
            Random random = new Random();
            var shouldBeFlipToDown = random.NextDouble() < DownSideOccurenceRatio;
            Side = shouldBeFlipToDown ? DiceSide.Down : DiceSide.Up;
            SetIcon();
        }

        private int NormalDistributionGenerateInteger(double mean, double stdDev)
        {
            Random random = new Random();
            double u1 = random.NextDouble();
            double u2 = random.NextDouble();
            double randomStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            int randomSample = (int) Math.Round(mean + stdDev * randomStdNormal);
            return randomSample > 0 ? randomSample : 1;
        }

        public override void _Process(float delta)
        {
            while (NumberOfRolls != 0)
            {
                MayBeFlip();
                deltaSteps = 0; 
                NumberOfRolls--; 
                
                if (NumberOfRolls == 0)
                {
                    EmitSignal("Rolled", this);
                }
            }
        }
    }
}