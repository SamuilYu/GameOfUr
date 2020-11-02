using Godot;
using System;

public class TilePopupMenu : CanvasLayer
{
    [Signal]
    delegate void SelectSoldier();
    
    [Signal]
    delegate void MakeSoldierMarch();

    public override void _Ready()
    {
        Connect("SelectSoldier", GetParent(), "SelectTopSoldier");
        Connect("MakeSoldierMarch", GetParent(), "MakeSoldierMarch");
    }

    private void _on_PopupMenu_id_pressed(int id)
    {
        if (id == 0) EmitSignal("SelectSoldier");
        if (id == 1) EmitSignal("MakeSoldierMarch");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
