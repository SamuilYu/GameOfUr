using Godot;
using royalgameofur;

public class Player : Node2D


{
    [Signal]
    public delegate void EndTurn();
    
    [Signal]
    public delegate void NewTurnSignal(string name);

    public override void _Ready()
    {
        if (Name == "Player2")
        {
            NewTurn();
        }
    }

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
        EmitSignal("NewTurnSignal", GetPlayerNameWrapper().GetPlayerName());
    }

    public PlayerNameWrapper GetPlayerNameWrapper()
    {
        return GetNode<PlayerNameWrapper>("/root/" + this.Name + "NameWrapper");
    }
}
