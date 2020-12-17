using Godot;
using System;

public class NewGamePopup : Control
{
    
    private void NewGame()
    {
        GetTree().Paused = true;
        this.Show();
        GetNode<PopupDialog>("Popup").Popup_();
    }
    
    private void Back()
    {
        GetTree().Paused = false;
        this.Hide();
        GetNode<PopupDialog>("Popup").Hide();
    }

    private void StartGame()
    {
        GetTree().ChangeScene("res://Node2D.tscn");
    }

}
