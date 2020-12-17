using System;
using System.Collections.Generic;
using Godot;

namespace royalgameofur
{
    public class Soldier: KinematicBody2D
    {
        [Signal]
        delegate void SoldierPressed(Soldier soldier);
        
        public List<KeyValuePair<Tile, SoldierTenure>> March { get; set; }

        public PlayerTeam Team;
        internal SoldierTenure Tenure;
        private Sprite Icon;
        internal Tile CurrentTile;
        public bool blocked;
        private Vector2 Speed;
        private bool CanMove;
        public int PreviousZIndex = 1;
        private SoldierTexturesKeeper keeper;
        private bool Awake { get; set; }



        public override void _Ready()
        {
            keeper = new SoldierTexturesKeeper();
            blocked = false;
            CanMove = false;

            Connect(nameof(SoldierPressed), GetParent<Squad>(), "WhenSoldierPressedDeselectOthers");
            
            Tenure = SoldierTenure.Private;
            Team = GetParent<Squad>().Team;
            Icon = GetNode<Sprite>("Icon");
            Icon.Texture = keeper.GetTexture(Tenure, Team);
            GetNode<Sprite>("Icon").Scale = 
                new Vector2((float)80/Icon.Texture.GetWidth(), 
                            (float)80/Icon.Texture.GetHeight());
            
            Retreat();
            Sleep();
        }

        public void ChangeTenure(SoldierTenure newTenure)
        {
            Tenure = newTenure;
            Icon.Texture = keeper.GetTexture(Tenure, Team);
            GetNode<Sprite>("Icon").Scale = 
                new Vector2((float)80/Icon.Texture.GetWidth(), 
                            (float)80/Icon.Texture.GetHeight());
        }

        public void Wake()
        {
            if (!blocked) Awake = true;
        }

        public void Sleep()
        {
            Awake = false;
        }

        public void Select()
        {
            if (March == null) return;
            
            // raise soldier above everyone else
            PreviousZIndex = ZIndex;
            ZIndex = 6;
            
            // inform squad, so that other soldiers get deselected
            EmitSignal(nameof(SoldierPressed), this);
            
            // check if destination can receive soldier
            Tile destination = March[March.Count - 1].Key;
            destination.CheckPlaceForSoldier(this);
        }

        public void Unselect()
        {
            // return soldier to height before selection
            ZIndex = PreviousZIndex;
            
            // soldier's potential destination gets deselected
            if (March?.Count != 0) March?[March.Count - 1].Key.Unselect();
        }

        public void Retreat()
        {
            CurrentTile?.GetNode<TileStrategy>("Type").Discharge(this);
            GetParent().GetNode<SquadBase>("SquadBase").Receive(this);
        }

        public void MarchOn()
        {
            GetParent<Squad>().GetNode<SquadBase>("SquadBase").Discharge(this);
            CurrentTile?.strategy.Discharge(this);
            
            // soldier can start moving in the direction of the first tile
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
            
            // clip to position when small enough distance
            var distance = (March[0].Key.GlobalPosition - this.GlobalPosition).Length();
            if (CanMove && distance < 10)
            {
                GlobalPosition = March[0].Key.GlobalPosition;
                
                // in case the destination is reached
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

        public bool IsAwake()
        {
            return Awake;
        }
    }
}
