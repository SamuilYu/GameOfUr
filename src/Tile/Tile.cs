using System;
using System.Collections.Generic;
using Godot;

namespace royalgameofur
{
    public class Tile: Node2D
    {
        public enum Light
        {
            None,
            Green,
            Red
        }

        public TileStrategy strategy { get; private set; }
        private Button button;
        private Soldier potentialSoldier;
        public bool reached = false;
        private bool waitingForSoldier = false;
        private Light lightColor;
        
        private bool canSelect;

        public override void _Ready()
        {
            // setting up an icon
            strategy = GetNode("Type") as TileStrategy;
            if (strategy?.Texture != null)
            {
                GetNode<Sprite>("Icon").Texture = strategy.Texture;
                GetNode<Sprite>("Icon").Scale =
                    new Vector2((float)100 / strategy.Texture.GetWidth(), 
                                (float)100 / strategy.Texture.GetHeight());
            }

            // setting up lights
            lightColor = Light.None;

            // setting up initial button state
            potentialSoldier = null;
            canSelect = false;
        }
        
        public void _On_Pressed()
        {
            switch (lightColor)
            {
                case Light.Green:
                    MaybeShowPopupMenu();
                    break;
                default:
                    MayBeSelectTopSoldier();
                    break;
            }
        }

        private void MaybeShowPopupMenu()
        {
            var currentTurn = GetParent<Node2D>().GetParent<Node2D>().GetParent<Node2D>().GetNode<Dice>("3D2 Dice").currentTurn;
            if (strategy.HasSoldiers(currentTurn))
            {
                var popupMenu = GetNode("TilePopupMenuWrapper").GetNode<PopupMenu>("PopupMenu");
                popupMenu.Popup_();
                popupMenu.SetGlobalPosition(GetViewport().GetMousePosition());
                return;
            }
            MakeSoldierMarch();
        }

        private void MayBeSelectTopSoldier()
        {
            var topSoldier = strategy.TopSoldier();
            if (topSoldier != null && topSoldier.IsAwake())
            {
                topSoldier?.Select();
            }
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
                lightColor = check ? Light.Green : Light.Red;
                GetNode<Sprite>("Icon").Modulate =
                    check? 
                        new Color((float) 0.44, (float) 0.70, (float) 0.13) 
                        : new Color((float) 0.93, (float) 0.29, (float) 0.04);

                canSelect = check;
                potentialSoldier = check ? soldier : null;
            }
            return check;
        }

        public void Unselect()
        {
            potentialSoldier = null;
            canSelect = false;
            GetNode<Sprite>("Icon").Modulate = new Color(1,1,1,1);
            lightColor = Light.None;
        }

        public override void _Process(float delta)
        {
            if (!waitingForSoldier || !reached) return;
            waitingForSoldier = false;
            reached = false;
            strategy.Receive(potentialSoldier);
            Unselect();
        }
        
        private Vector2 GetCameraGlobalPosition()
        {
            var topLeft = -GetCanvasTransform().origin / GetCanvasTransform().Scale;
            var size = GetViewport().Size;
            return topLeft + (float)0.5 * size / GetCanvasTransform().Scale;
        }
    }
    
}