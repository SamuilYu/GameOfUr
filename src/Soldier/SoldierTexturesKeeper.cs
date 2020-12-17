using System;
using Godot;

namespace royalgameofur
{
    public class SoldierTexturesKeeper
    {
        private Texture BlackPrivateTexture { get; set; }
        private Texture BlackVeteranTexture { get; set; }
        private Texture WhitePrivateTexture { get; set; }
        private Texture WhiteVeteranTexture { get; set; }

        public SoldierTexturesKeeper()
        {
            BlackPrivateTexture = GD.Load("res://textures/soldiers/BlackPrivate.png") as Texture;
            BlackVeteranTexture = GD.Load("res://textures/soldiers/BlackVeteran.png") as Texture;
            WhitePrivateTexture = GD.Load("res://textures/soldiers/WhitePrivate.png") as Texture;
            WhiteVeteranTexture = GD.Load("res://textures/soldiers/WhiteVeteran.png") as Texture;

        }

        public Texture GetTexture(SoldierTenure tenure, PlayerTeam team)
        {
            switch (tenure)
            {
                case SoldierTenure.Private:
                    switch (team)
                    {
                        case PlayerTeam.White:
                            return WhitePrivateTexture;
                        case PlayerTeam.Black:
                            return BlackPrivateTexture;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case SoldierTenure.Veteran:
                case SoldierTenure.Switch:
                    switch (team)
                    {
                        case PlayerTeam.White:
                            return WhiteVeteranTexture;
                        case PlayerTeam.Black:
                            return BlackVeteranTexture;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }

}