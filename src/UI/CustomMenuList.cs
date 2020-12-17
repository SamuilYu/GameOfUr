using Godot;
using System;

public class CustomMenuList : VBoxContainer
{

    [Signal]
    delegate void NewGame(); 

    [Signal]
    delegate void Settings(); 
    
    [Signal]
    delegate void Quit();

    private void NewGamePressed()
    {
        EmitSignal("NewGame");
    }
    
    private void SettingsPressed()
    {
        EmitSignal("Settings");
    }
    
    private void QuitPressed()
    {
        EmitSignal("Quit");
    }
    
}
