using Godot;
using System;
using royalgameofur;

public class Tracker : Node2D
{
    public int releasedSoldiersCount = 0;
    
    public override void _Ready()
    {
        
    }

    public void Increment()
    {
        releasedSoldiersCount++;
        if (GetParent<Squad>().Team == PlayerTeam.White)
            GetNode("Bar").GetNode<Sprite>("WhiteFlag" + releasedSoldiersCount).Show();
        if (GetParent<Squad>().Team == PlayerTeam.Black)
            GetNode("Bar").GetNode<Sprite>("BlackFlag" + releasedSoldiersCount).Show();
    }

}
