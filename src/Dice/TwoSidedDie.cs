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
        private static float _step = (float)1;
        public static Animation Cast { get; set; }


        public override void _Ready()
        {
            Side = DiceSide.Down;
            Cast = GetNode<AnimationPlayer>("AnimationPlayer").GetAnimation("Cast");
            
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
            // Cast.TrackInsertKey(0, NumberOfRolls * _step, GetNode<Sprite>("Icon").Texture);
            // Cast.TrackInsertKey(1, NumberOfRolls * _step, GetNode<Sprite>("Icon").Scale);
        }

        public DiceSide Roll()
        {
            NumberOfRolls = NormalDistributionGenerateInteger(MeanNumberOfRolls, StdDevNumberOfRolls);
            Cast.Length = (float)NumberOfRolls / 5;
            // Cast.ValueTrackSetUpdateMode(0, Animation.UpdateMode.Discrete);
            // Cast.ValueTrackSetUpdateMode(1, Animation.UpdateMode.Discrete);
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

        public void AnimationStopped(string name)
        {
            // if (GetNode<AnimationPlayer>("AnimationPlayer").GetAnimation(name).Length > 0.06)
            // {
            //     Cast.Length = (float) 0.05;
            //     Cast.TrackInsertKey(0, (float) 0.02, GetNode<Sprite>("Icon").Texture);
            //     Cast.TrackInsertKey(1, (float) 0.02, GetNode<Sprite>("Icon").Scale);
            //     Cast.Loop = true;
            //     GetNode<AnimationPlayer>("AnimationPlayer").Play("Cast");
            // }
        }

        public override void _PhysicsProcess(float delta)
        {
            if (NumberOfRolls != 0)
            {
                if (deltaSteps == 0)
                {
                    MayBeFlip();
                    deltaSteps = 5;
                    NumberOfRolls--;
                }

                deltaSteps--;

                if (NumberOfRolls == 0)
                {
                    EmitSignal("Rolled", this);
                    //Cast.Loop = false;
                    //GetNode<AnimationPlayer>("AnimationPlayer").PlayBackwards("Cast");
                }
            }
        }
    }
}