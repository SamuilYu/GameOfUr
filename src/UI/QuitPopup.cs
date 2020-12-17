using Godot;

namespace royalgameofur
{
    public class QuitPopup : Control
    {
        
        private void Quit()
        {
            if (GetParent().GetParent().Name == "MainMenu") GetTree().Paused = true;
            this.Show();
            GetNode<PopupDialog>("Popup").Popup_();
        }
    
        private void Back()
        {
            if (GetParent().GetParent().Name == "MainMenu") GetTree().Paused = false;
            this.Hide();
            GetNode<PopupDialog>("Popup").Hide();
        }

        private void YesQuit()
        {
            GetTree().Quit();
        }

        
    }
}