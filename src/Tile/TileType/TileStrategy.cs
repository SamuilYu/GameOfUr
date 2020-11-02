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
			soldier.CurrentTile?.strategy.Discharge(soldier);
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

		protected virtual void DrawTower()
		{
			var places = GetParent<Tile>().TileDetails
				.GetNode("DetailsContainer")
				.GetNode("Places").GetChildren();
			if (FilledCapacity == 1)
			{
				if (places[0] is Button button0) button0.Icon = null;
				if (places[1] is Button button1) button1.Icon = null;
				if (places[2] is Button button2) button2.Icon = stationedSoldiers[0].Button.Icon;
				if (places[3] is Button button3) button3.Icon = null;
			} 
			else if (FilledCapacity == 2)
			{
				if (places[0] is Button button0) button0.Icon = null;
				if (places[1] is Button button1) button1.Icon = stationedSoldiers[0].Button.Icon;
				if (places[2] is Button button2) button2.Icon = stationedSoldiers[1].Button.Icon;
				if (places[3] is Button button3) button3.Icon = null;

			}
			else if (FilledCapacity == 3)
			{
				if (places[0] is Button button0) button0.Icon = null;
				if (places[1] is Button button1) button1.Icon = stationedSoldiers[0].Button.Icon;
				if (places[2] is Button button2) button2.Icon = stationedSoldiers[1].Button.Icon;
				if (places[3] is Button button3) button3.Icon = stationedSoldiers[2].Button.Icon;
			}
			else if (FilledCapacity == 4)
			{
				if (places[0] is Button button0) button0.Icon = stationedSoldiers[0].Button.Icon;
				if (places[1] is Button button1) button1.Icon = stationedSoldiers[1].Button.Icon;
				if (places[2] is Button button2) button2.Icon = stationedSoldiers[2].Button.Icon;
				if (places[3] is Button button3) button3.Icon = stationedSoldiers[3].Button.Icon;
			}
			else if (FilledCapacity == 0)
			{
				if (places[0] is Button button0) button0.Icon = null;
				if (places[1] is Button button1) button1.Icon = null;
				if (places[2] is Button button2) button2.Icon = null;
				if (places[3] is Button button3) button3.Icon = null;
			}
		}
	}
}
