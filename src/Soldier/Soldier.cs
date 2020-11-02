using System;
using System.Collections.Generic;
using Godot;

namespace royalgameofur
{
    public class Soldier: KinematicBody2D
    {
        [Signal]
        delegate void SoldierPressed(Soldier soldier);
        private Texture BlackPrivateTexture { get; set; }
        private Texture BlackVeteranTexture { get; set; }
        private Texture WhitePrivateTexture { get; set; }
        private Texture WhiteVeteranTexture { get; set; }
        public List<KeyValuePair<Tile, SoldierTenure>> March { get; set; }

        public PlayerTeam Team;
        internal SoldierTenure Tenure;
        public Button Button;
        internal Tile CurrentTile;
        public bool blocked;
        private Vector2 Speed;
        private bool CanMove;
        public int PreviousZIndex = 1;

        public Soldier()
        {
            BlackPrivateTexture = GD.Load("res://textures/soldiers/BlackPrivate.png") as Texture;
            BlackVeteranTexture = GD.Load("res://textures/soldiers/BlackVeteran.png") as Texture;
            WhitePrivateTexture = GD.Load("res://textures/soldiers/WhitePrivate.png") as Texture;
            WhiteVeteranTexture = GD.Load("res://textures/soldiers/WhiteVeteran.png") as Texture;
        }

        public override void _Ready()
        {
            blocked = false;
            CanMove = false;
            Button = GetNode<Button>("SoldierButton");
            Button.Connect("pressed", this, "Select");
            Button.Flat = true;
            Button.MouseFilter = Control.MouseFilterEnum.Ignore;
            Connect(nameof(SoldierPressed), GetParent<Squad>(), "_On_SoldierPressed");
            Tenure = SoldierTenure.Private;
            Team = GetParent<Squad>().Team;
            SetIcon();
            Retreat();
            Sleep();
        }

        public void ChangeTenure(SoldierTenure newTenure)
        {
            this.Tenure = newTenure;
            SetIcon();
        }

        public void Wake()
        {
            if (!blocked) Button.MouseFilter = Control.MouseFilterEnum.Stop;
        }

        public void Sleep()
        {
            Button.MouseFilter = Control.MouseFilterEnum.Ignore;
        }

        public void Select()
        {
            PreviousZIndex = ZIndex;
            ZIndex = 6;
            EmitSignal(nameof(SoldierPressed), this);
        }

        public void Unselect()
        {
            ZIndex = PreviousZIndex;
            if (March?.Count != 0) March?[March.Count - 1].Key.Unselect();
        }

        private void SetIcon()
        {
            switch (Tenure)
            {
                case SoldierTenure.Private:
                    switch (Team)
                    {
                        case PlayerTeam.White:
                            Button.Icon = WhitePrivateTexture;
                            break;
                        case PlayerTeam.Black:
                            Button.Icon = BlackPrivateTexture;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case SoldierTenure.Veteran:
                case SoldierTenure.Switch:
                    switch (Team)
                    {
                        case PlayerTeam.White:
                            Button.Icon = WhiteVeteranTexture;
                            break;
                        case PlayerTeam.Black:
                            Button.Icon = BlackVeteranTexture;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Retreat()
        {
            CurrentTile?.GetNode<TileStrategy>("Type").Discharge(this);
            GetParent().GetNode<SquadBase>("SquadBase").Receive(this);
        }

        public void MarchOn()
        {
            GetParent<Squad>().GetNode<SquadBase>("SquadBase").Discharge(this);
            CanMove = true;
            if (March.Count != 0) Speed = (March[0].Key.GlobalPosition - this.GlobalPosition).Normalized() * 150;
        }

        public override void _PhysicsProcess(float delta)
        {
            if (March == null || March.Count == 0)
            {
                Speed = new Vector2(0,0);
                CanMove = false;
                March = null;
                return;
            }
            
            var distance = (March[0].Key.GlobalPosition - this.GlobalPosition).Length();
            if (CanMove && distance < 10)
            {
                this.GlobalPosition = March[0].Key.GlobalPosition;
                if (March.Count == 1)
                {
                    CanMove = false;
                    March[0].Key.reached = true;
                    ChangeTenure(March[0].Value);
                }
                March.RemoveAt(0);
                if (March.Count != 0) Speed = (March[0].Key.GlobalPosition - this.GlobalPosition).Normalized() * 200;
                return;
            }

            if (CanMove) MoveAndSlide(Speed);
        }

        public void EndTurn()
        {
            GetParent<Squad>().EndTurn();
        }
    }
}
