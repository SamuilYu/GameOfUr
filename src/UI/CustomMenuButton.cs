using Godot;
using System;

public class CustomMenuButton : Button
{
    private void OnMouseEnter()
    {
        GrabFocus();
    }
    private void OnMouseExit()
    {
        ReleaseFocus();
    }
}
