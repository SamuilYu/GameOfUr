using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace royalgameofur
{
	public enum TileType
	{
		None,
		RoadTile,
		CampTile,
		CityTile,
		TempleTile,
		SacredTile,
		PyramidTile,
		BattlegroundTile
	}

	public abstract class TileStrategy : Node2D
	{
		public Texture Texture { get; protected set; }
		protected int MaxCapacity { get; set; }
		protected int FilledCapacity { get; set; }
		public string Description { get; set; }

		protected List<Soldier> stationedSoldiers;
		protected TileType type;


		public override void _Ready()
		{
			stationedSoldiers = new List<Soldier>();
			FilledCapacity = 0;
			Description = "This is an untyped tile";
		}

		public virtual bool CanReceiveSoldier(Soldier soldier)
		{
			if (FilledCapacity < MaxCapacity) return true;
			return stationedSoldiers[FilledCapacity - 1].Team != soldier.Team 
			       && stationedSoldiers[FilledCapacity - 1].Tenure == soldier.March[soldier.March.Count - 1].Value;
		}

		public virtual void Receive(Soldier soldier)
		{
			GD.Print("Received");
			if (FilledCapacity >= MaxCapacity) return;
			stationedSoldiers.Add(soldier);
			FilledCapacity++;
			
			soldier.CurrentTile = GetParent<Tile>();
			soldier.ZIndex = FilledCapacity;
			soldier.PreviousZIndex = soldier.ZIndex;
			DrawTower();
		}

		protected void MayBeAttack(Soldier soldier)
		{
			if (FilledCapacity != 0 
			    && stationedSoldiers[0].Team != soldier.Team 
			    && stationedSoldiers[0].Tenure == soldier.Tenure)
			{
				stationedSoldiers[0].Retreat();
			}
		}

		public virtual void Discharge(Soldier soldier)
		{
			if (!stationedSoldiers.Exists(each => each == soldier)) return;
			FilledCapacity--;
			stationedSoldiers.Remove(soldier);
			DrawTower();
		}

		public bool HasSoldiers(PlayerTeam currentTurn)
		{
			if (stationedSoldiers != null) 
				return stationedSoldiers.Any(soldier => soldier.Team == currentTurn && !soldier.blocked);
			return false;
		}

		public Soldier TopSoldier()
		{
			if (FilledCapacity != 0) return stationedSoldiers[FilledCapacity - 1];
			return null;
		}

		private void DrawTower()
		{
			var places = GetParent<Tile>().GetNode<Node2D>("TowerView").GetNode("Background").GetChildren();
			(places[0] as Polygon2D)?.Hide();
			(places[1] as Polygon2D)?.Hide();
			(places[2] as Polygon2D)?.Hide();
			(places[3] as Polygon2D)?.Hide();
			for (int i = 0; i < 4; i++)
			{
				if (FilledCapacity > i)
				{
					if (places[i] is Polygon2D place)
					{
						if (stationedSoldiers[i].Team == PlayerTeam.White)
						{
							place.Color = new Color((float)0.87, (float)0.82, (float)0.62);
						}
						else
						{
							place.Color = new Color((float)0.32, (float)0.24, (float)0.05);
						}

						if (stationedSoldiers[i].Tenure == SoldierTenure.Veteran)
						{
							place.GetNode<Polygon2D>("Diamond").Show();
						}
						else
						{
							place.GetNode<Polygon2D>("Diamond").Hide();
						}
						place.Show();
					}
				
				} 
			}
		}
	}
}
