using Godot;
using System;

public class EndGamePopup : Control
{
    public void Win(string name)
    {
        GetNode<PopupDialog>("Popup")
            .GetNode<Label>("Label").Text = 
            name + ", you have won!";
        
        GetTree().Paused = true;
        this.Show();
        GetNode<PopupDialog>("Popup").Popup_();
    }
    
    private void MainMenu()
    {
        GetTree().ChangeScene("res://scenes/ui/main-menu/MainMenu.tscn");
    }

    private void Rematch()
    {
        GetTree().ChangeScene("res://Node2D.tscn");
    }
}
