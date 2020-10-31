using System.Collections.Generic;
using Godot;

namespace royalgameofur
{
    public class Tile: Node2D
    {
        private TileStrategy strategy;
        private Button button;
        private Soldier potentialSoldier;
        public bool reached = false;
        private bool waitingForSoldier = false;

        public override void _Ready()
        {
            strategy = GetNode("Type") as TileStrategy;
            button = GetNode("TileButton") as Button;
            potentialSoldier = null;

            if (button == null) return;
            button.Connect("pressed", this, "_On_Pressed");
            button.Icon = strategy.Texture;
            button.MouseFilter = Control.MouseFilterEnum.Ignore;
            button.Flat = true;
        }

        public void _On_Pressed()
        {
            if (potentialSoldier.March != null) potentialSoldier.MarchOn();
            waitingForSoldier = true;
        }

        public void CheckPlaceForSoldier(Soldier soldier)
        {
            bool check = strategy.CanReceiveSoldier(soldier);
            button.MouseFilter =  check? Control.MouseFilterEnum.Pass : Control.MouseFilterEnum.Ignore;
            button.Flat = !check;
            potentialSoldier = check ? soldier : null;
        }

        public void Unselect()
        {
            potentialSoldier = null;
            button.MouseFilter = Control.MouseFilterEnum.Ignore;
            button.Flat = true;
        }

        public override void _Process(float delta)
        {
            if (!waitingForSoldier || !reached) return;
            waitingForSoldier = false;
            reached = false;
            strategy.Receive(potentialSoldier);
            Unselect();
        }
    }
    
}