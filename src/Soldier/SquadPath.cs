using System.Collections.Generic;
using System.Linq;
using Godot;

namespace royalgameofur
{
    
    public class SquadPath
    {
        private PlayerTeam Team { get; set; }
        private List<KeyValuePair<Tile, SoldierTenure>> Path;
        private Node2D AllTiles;

        public void SetPath(Squad squad)
        {
            Team = squad.Team;
            AllTiles = squad
                    .GetParent<Player>()
                    .GetParent()
                    .GetNode("Board")
                    .GetNode<Node2D>("TilesCollection");
            Path = new List<KeyValuePair<Tile, SoldierTenure>>();

            var teamString = Team.ToString();
            var otherString = Team == PlayerTeam.White ? "Black" : "White";

            if (AllTiles == null) return;
            // starting line
            Path.Add(
                new KeyValuePair<Tile, SoldierTenure>(
                    AllTiles.GetNode(teamString + "Start") as Tile, 
                    SoldierTenure.Private));
            Path.Add(
                new KeyValuePair<Tile, SoldierTenure>(
                    AllTiles.GetNode(teamString + "Road") as Tile,
                    SoldierTenure.Private));
            Path.Add(
                new KeyValuePair<Tile, SoldierTenure>(
                    AllTiles.GetNode(teamString + "Camp") as Tile,
                    SoldierTenure.Private));
            Path.Add(
                new KeyValuePair<Tile, SoldierTenure>(
                    AllTiles.GetNode(teamString + "Temple") as Tile,
                    SoldierTenure.Private));
            
            // middle line
            Path.Add(
                new KeyValuePair<Tile, SoldierTenure>(
                    AllTiles.GetNode("Finish") as Tile,
                    SoldierTenure.Private));
            Path.Add(
                new KeyValuePair<Tile, SoldierTenure>(
                    AllTiles.GetNode("MainRoad") as Tile,
                    SoldierTenure.Private));
            Path.Add(
                new KeyValuePair<Tile, SoldierTenure>(
                    AllTiles.GetNode("Uruk") as Tile,
                    SoldierTenure.Private));
            Path.Add(
                new KeyValuePair<Tile, SoldierTenure>(
                    AllTiles.GetNode("MainTemple") as Tile,
                    SoldierTenure.Private));
            Path.Add(
                new KeyValuePair<Tile, SoldierTenure>(
                    AllTiles.GetNode("BridgeRoad") as Tile,
                    SoldierTenure.Private));
            Path.Add(
                new KeyValuePair<Tile, SoldierTenure>(
                    AllTiles.GetNode("BridgeCity") as Tile,
                    SoldierTenure.Private));
            Path.Add(
                new KeyValuePair<Tile, SoldierTenure>(
                    AllTiles.GetNode("BridgeCamp") as Tile,
                    SoldierTenure.Private));
            
            // after bridge
            Path.Add(
                new KeyValuePair<Tile, SoldierTenure>(
                    AllTiles.GetNode(teamString + "Temple2") as Tile,
                    SoldierTenure.Private));
            Path.Add(
                new KeyValuePair<Tile, SoldierTenure>(
                    AllTiles.GetNode(teamString + "Pyramid") as Tile,
                    SoldierTenure.Private));
            Path.Add(
                new KeyValuePair<Tile, SoldierTenure>(
                    AllTiles.GetNode("Battleground") as Tile,
                    SoldierTenure.Switch));
            Path.Add(
                new KeyValuePair<Tile, SoldierTenure>(
                    AllTiles.GetNode(otherString + "Pyramid") as Tile,
                    SoldierTenure.Veteran));
            Path.Add(
                new KeyValuePair<Tile, SoldierTenure>(
                    AllTiles.GetNode(otherString + "Temple2") as Tile,
                    SoldierTenure.Veteran));
            
            // finish line
            Path.Add(new KeyValuePair<Tile, SoldierTenure>(
                AllTiles.GetNode("BridgeCamp") as Tile,
                SoldierTenure.Veteran));
            Path.Add(
                new KeyValuePair<Tile, SoldierTenure>(
                    AllTiles.GetNode("BridgeCity") as Tile,
                    SoldierTenure.Veteran));
            Path.Add(
                new KeyValuePair<Tile, SoldierTenure>(
                    AllTiles.GetNode("BridgeRoad") as Tile,
                    SoldierTenure.Veteran));
            Path.Add(
                new KeyValuePair<Tile, SoldierTenure>(
                    AllTiles.GetNode("MainTemple") as Tile,
                    SoldierTenure.Veteran));
            Path.Add(
                new KeyValuePair<Tile, SoldierTenure>(
                    AllTiles.GetNode("Uruk") as Tile,
                    SoldierTenure.Veteran));
            Path.Add(
                new KeyValuePair<Tile, SoldierTenure>(
                    AllTiles.GetNode("MainRoad") as Tile,
                    SoldierTenure.Veteran));
            Path.Add(
                new KeyValuePair<Tile, SoldierTenure>(
                    AllTiles.GetNode("Finish") as Tile,
                    SoldierTenure.Veteran));
            Path.Add(
                new KeyValuePair<Tile, SoldierTenure>(
                    AllTiles.GetNode("Release") as Tile,
                    SoldierTenure.Veteran));
        }

        public List<KeyValuePair<Tile, SoldierTenure>> GetNext(Tile currentTile, SoldierTenure tenure, int steps)
        {
            var findValue = new KeyValuePair<Tile, SoldierTenure>(currentTile, tenure);
            var currentIndex = currentTile == null? 
                -1: Path.FindIndex(keyValue => 
                    keyValue.Key == findValue.Key 
                    && keyValue.Value == findValue.Value);
            return currentIndex + 1 + steps > Path.Count ? null : Path.GetRange(currentIndex + 1, steps);
        }
    }


}