using Godot;
using royalgameofur;

public class Player : Node2D


{
    [Signal]
    public delegate void EndTurn();

    public void EndThisTurn()
    {
        foreach (var child in GetParent().GetChildren())
        {
            if (child is Player player && player != this) player.NewTurn();
        }
        EmitSignal("EndTurn");
    }

    private void NewTurn()
    {
        GD.Print("My turn!");
    }
}
