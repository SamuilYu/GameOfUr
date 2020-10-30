using Godot;
using System;
using royalgameofur;

public class Player : Node2D


{
    [Export()]
    public PlayerTeam Team { get; private set; }

    public override void _Ready()
    {
        var path = new PlayerPath(Team);
        AddChild(path);
    }

}
