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
        if (Name == "Player1")
        {
            GetParent().GetNode<Player>("Player2").NewTurn();
        }
        else
        {
            GetParent().GetNode<Player>("Player1").NewTurn();
        }
        EmitSignal("EndTurn");
    }

    private void NewTurn()
    {
        GD.Print("My turn!");
        EmitSignal("NewTurnSignal", GetPlayerNameWrapper().GetPlayerName());
        GetTree().Paused = false;
    }

    public PlayerNameWrapper GetPlayerNameWrapper()
    {
        return GetNode<PlayerNameWrapper>("/root/" + this.Name + "NameWrapper");
    }
}
