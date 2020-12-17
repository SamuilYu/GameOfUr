using Godot;
using System;

public class YourTurnPopup : Control
{

    public void PopupWithName(string name)
    {
        GetNode("Popup").GetNode<Label>("Label").Text = "It is your turn, commander " + name + ".";
        GetNode<PopupDialog>("Popup").Popup_();
    }
    
}
