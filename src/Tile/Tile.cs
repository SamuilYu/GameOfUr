using System.Collections.Generic;
using Godot;

namespace royalgameofur
{
    public class Tile: Node2D
    {
        public TileStrategy strategy { get; private set; }
        private Button button;
        private Soldier potentialSoldier;
        public bool reached = false;
        private bool waitingForSoldier = false;
        private Button tileLight;
        public PanelContainer TileDetails;
        private bool canSelect;

        public override void _Ready()
        {
            strategy = GetNode("Type") as TileStrategy;
            button = GetNode("TileButtonWrapper").GetNode<Button>("TileButton");
            tileLight = GetNode<Button>("TileLightButton");
            potentialSoldier = null;
            TileDetails = GetNode("TileDetailsPopup").GetNode<PanelContainer>("TileDetails");
            canSelect = false;

            if (button == null) return;
            // button.Connect("pressed", this, "_On_Pressed");
            GetNode<Button>("IconButton").Icon = strategy.Texture;
            button.ButtonMask = 3;
            button.Flat = true;
            button.RectGlobalPosition = tileLight.RectGlobalPosition;
            GetNode("TileButtonWrapper").GetNode<Button>("DetailsButton").RectGlobalPosition = button.RectGlobalPosition;
            tileLight.MouseFilter = Control.MouseFilterEnum.Ignore;
            tileLight.Flat = true;

            TileDetails.GetNode<HBoxContainer>("DetailsContainer").GetNode<Label>("TypeDescription").Text =
                strategy.Description;
            TileDetails.RectGlobalPosition = new Vector2(275, 365);
            TileDetails.GetParent<Popup>().Hide();
        }

        private void OnTileButtonInput(InputEvent inputEvent)
        {
            if (inputEvent is InputEventMouseButton currentEvent && currentEvent.IsPressed())
            {
                switch (currentEvent.ButtonIndex)
                {
                    case (int)ButtonList.Left:
                        if (canSelect) _On_Pressed();
                        break;
                    case (int)ButtonList.Right:
                        OnDetailsPopupPressed();
                        break;
                    default: break;
                }
            }
        }

        private void OnDetailsPopupPressed()
        {
            foreach (var child in GetParent().GetChildren())
            {
                if (child is Tile tile && tile != this)
                {
                    tile.GetNode<Popup>("TileDetailsPopup").Hide();
                }
                
            }
            TileDetails.GetParent<Popup>().Popup_();
        }

        public void _On_Pressed()
        {
            var currentTurn = GetParent<Node2D>().GetParent<Node2D>().GetParent<Node2D>().GetNode<Dice>("3D2 Dice").currentTurn;
            if (strategy.HasSoldiers(currentTurn))
            {
                var popupMenu = GetNode("TilePopupMenuWrapper").GetNode<PopupMenu>("PopupMenu");
                popupMenu.Popup_();
                popupMenu.SetGlobalPosition(GetGlobalMousePosition());
                return;
            }
            MakeSoldierMarch();
        }

        private void SelectTopSoldier()
        {
            strategy.TopSoldier()?.Select();
        }

        private void MakeSoldierMarch()
        {
            if (potentialSoldier.March != null)
            {
                waitingForSoldier = true;
                potentialSoldier.MarchOn();
            }
        }

        public bool CheckPlaceForSoldier(Soldier soldier, bool toggle=true)
        {
            bool check = strategy.CanReceiveSoldier(soldier);
            if (toggle)
            {
                canSelect = check;
                // button.ButtonMask |= check? (int) ButtonList.MaskLeft : 0;
                tileLight.Flat = false;
                tileLight.Modulate = 
                    check? 
                        new Color((float) 0.44, (float) 2.0, (float) 0.03, (float) 0.25) 
                        : new Color((float) 0.93, (float) 0.29, (float) 0.04, (float) 0.4);
                potentialSoldier = check ? soldier : null;
            }
            return check;
        }

        public void Unselect()
        {
            potentialSoldier = null;
            canSelect = false;
            // button.ButtonMask = (int) ButtonList.MaskRight;
            tileLight.Flat = true;
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