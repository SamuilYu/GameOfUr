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
        foreach (var child in GetParent().GetNode("Board").GetNode("TilesCollection").GetChildren())
        {
            if (child is Tile tile)
            {
                switch (GetNode<Squad>("Squad").Team)
                {
                    case PlayerTeam.Black:
                        tile.TileDetails.RectGlobalPosition = new Vector2(275, 365);
                        break;
                    case PlayerTeam.White:
                        tile.TileDetails.RectGlobalPosition = new Vector2(275, 137);
                        break;
                }
            }
        }
        
    }
}
