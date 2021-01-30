using Godot;
using System;
using System.Collections.Generic;
using royalgameofur;

public class NewGamePopup : Control
{
    internal static NewGamePopup CreateInstance()
    {
        return new NewGamePopup();
    }

    private readonly List<bool> emptyNamesFlags = new List<bool>();
    private List<string> playerNames = new List<string>();

    private NewGamePopup()
    {
        emptyNamesFlags.Add(false);
        emptyNamesFlags.Add(false);
        
        playerNames.Add("");
        playerNames.Add("");
    }

    public void EmptyName(int number, string name)
    {
        GetNode("Popup").GetNode<Button>("StartGame").Disabled = true;
        emptyNamesFlags[number - 1] = false;
    }
    
    public void NonEmptyName(int number, string name)
    {
        playerNames[number - 1] = name;
        emptyNamesFlags[number - 1] = true;
        if (playerNames[0] == playerNames[1])
        {
            GetNode("Popup").GetNode<Button>("StartGame").Disabled = true;
        }
    }

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

    public override void _PhysicsProcess(float delta)
    {
        if (emptyNamesFlags[0] && emptyNamesFlags[1] && playerNames[0] != playerNames[1])
        {
            GetNode("Popup").GetNode<Button>("StartGame").Disabled = false;
        }
    }
}
