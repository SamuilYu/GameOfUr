using System.Collections.Generic;
using System.Linq;
using Godot;

namespace royalgameofur
{
    
    public class PlayerPath : Node2D
    {
        private PlayerTeam Team { get; set; }
        private List<Tile> Path;
        private Node2D AllTiles; 

        public PlayerPath(PlayerTeam team)
        {
            Team = team;
        }

        public override void _Ready()
        { 
            SetPath();
        }

        private void SetPath()
        {
            AllTiles = GetNode("/root/Node2D/Board/TilesCollection") as Node2D;
            Path = new List<Tile>();

            var teamString = Team.ToString();
            var otherString = Team == PlayerTeam.White ? "Black" : "White";

            if (AllTiles == null) return;
            Path.Add(AllTiles.GetNode(teamString + "Start") as Tile);
            Path.Add(AllTiles.GetNode(teamString + "Road") as Tile);
            Path.Add(AllTiles.GetNode(teamString + "Camp") as Tile);
            Path.Add(AllTiles.GetNode(teamString + "Temple") as Tile);
            
            Path.Add(AllTiles.GetNode("Finish") as Tile);
            Path.Add(AllTiles.GetNode("MainRoad") as Tile);
            Path.Add(AllTiles.GetNode("Uruk") as Tile);
            Path.Add(AllTiles.GetNode("MainTemple") as Tile);
            Path.Add(AllTiles.GetNode("BridgeRoad") as Tile);
            Path.Add(AllTiles.GetNode("BridgeCamp") as Tile);
            Path.Add(AllTiles.GetNode("BridgeCity") as Tile);
            
            Path.Add(AllTiles.GetNode(teamString + "Temple2") as Tile);
            Path.Add(AllTiles.GetNode(teamString + "Pyramid") as Tile);
            Path.Add(AllTiles.GetNode("Battleground") as Tile);
            Path.Add(AllTiles.GetNode(otherString + "Pyramid") as Tile);
            Path.Add(AllTiles.GetNode(teamString + "Temple2") as Tile);
            
            Path.Add(AllTiles.GetNode("BridgeCity") as Tile);
            Path.Add(AllTiles.GetNode("BridgeCamp") as Tile);
            Path.Add(AllTiles.GetNode("BridgeRoad") as Tile);
            Path.Add(AllTiles.GetNode("MainTemple") as Tile);
            Path.Add(AllTiles.GetNode("Uruk") as Tile);
            Path.Add(AllTiles.GetNode("MainRoad") as Tile);
            Path.Add(AllTiles.GetNode("Finish") as Tile);
        }
    }


}