using System;
using System.Runtime.Remoting.Channels;
using Godot;

namespace royalgameofur
{
    public class Soldier: KinematicBody2D
    {
        private Texture BlackPrivateTexture { get; set; }
        private Texture BlackVeteranTexture { get; set; }
        private Texture WhitePrivateTexture { get; set; }
        private Texture WhiteVeteranTexture { get; set; }
        
        private PlayerTeam Team;
        private SoldierTenure Tenure;
        private Button Button;

        public Soldier()
        {
            BlackPrivateTexture = GD.Load("res://textures/soldiers/BlackPrivate.png") as Texture;
            BlackVeteranTexture = GD.Load("res://textures/soldiers/BlackVeteran.png") as Texture;
            WhitePrivateTexture = GD.Load("res://textures/soldiers/WhitePrivate.png") as Texture;
            WhiteVeteranTexture = GD.Load("res://textures/soldiers/WhiteVeteran.png") as Texture;
        }

        public override void _Ready()
        {
            Button = GetNode("SoldierButton") as Button;
            Tenure = SoldierTenure.Private;
            if (GetParent() is Squad squad) Team = squad.Team;
            GD.Print(Team);
            if (GetParent().GetNode("SquadBase") is SquadBase squadBase) squadBase.Receive(this);
            SetIcon();
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
    }
}