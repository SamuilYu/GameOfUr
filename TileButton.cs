using Godot;
using System;

public class TileButton : Button
{
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton thisEvent 
            && thisEvent.IsPressed()
            && GetRect().HasPoint(thisEvent.Position))
        {
            EmitSignal("gui_input", @event);
        }
    }
}
