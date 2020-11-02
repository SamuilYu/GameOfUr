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
		protected List<Soldier> stationedSoldiers;
		protected TileType type;


		public override void _Ready()
		{
			stationedSoldiers = new List<Soldier>();
			FilledCapacity = 0;
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
		}

		public bool HasSoldiers(PlayerTeam currentTurn)
		{
			return stationedSoldiers.Any(soldier => soldier.Team == currentTurn && !soldier.blocked);
		}

		public Soldier TopSoldier()
		{
			if (FilledCapacity != 0) return stationedSoldiers[FilledCapacity - 1];
			return null;
		}
	}
}
