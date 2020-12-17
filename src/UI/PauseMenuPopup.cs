using System.Net;
using Godot;

namespace royalgameofur
{
    public class PauseMenuPopup : Control
    {
        public override void _Input(InputEvent @event)
        {
            if (@event is InputEventKey keyEvent && keyEvent.IsPressed() && keyEvent.Scancode == (ulong) KeyList.Escape)
            {
                if (!Visible)
                {
                    GetTree().Paused = true;
                    this.Show();
                    GetNode<PopupDialog>("Popup").Popup_();
                }
                else
                {
                    Continue();
                }
            }
        }

        private void Continue()
        {
            GetTree().Paused = false;
            this.Hide();
            GetNode<PopupDialog>("Popup").Hide();
        }
    }
}