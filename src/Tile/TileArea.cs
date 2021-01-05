using Godot;
using System;
using royalgameofur;

public class TileArea : Area2D
{

    private bool mouseOver = false;

    public override void _Input(InputEvent @event)
    {
        if (mouseOver && @event.IsPressed() 
                      && @event is InputEventMouseButton mouseEvent 
                      && mouseEvent.ButtonIndex == (int) ButtonList.Left)
            {
                if (GetParent() is Tile tile)
                {
                    tile._On_Pressed();
                } 
                else if (GetParent() is SquadBase squadBase)
                {
                    squadBase._On_Pressed();
                }
                else
                {
                    GetParent<Dice>().Pressed();
                }
            }
    }

    public void MouseEntered()
    {
        mouseOver = true;
    }

    public void MouseExited()
    {
        mouseOver = false;
    }
    
}
