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
            if (Name == "MainMenuPopup")
            {
                GetTree().ChangeScene("res://scenes/ui/main-menu/MainMenu.tscn");
            }
            else if (Name == "QuitPopup")
            {
                GetTree().Quit();
            } 
            else if (Name == "ReplayPopup")
            {
                GetTree().ChangeScene("res://Node2D.tscn");
            }
        }

        
    }
}