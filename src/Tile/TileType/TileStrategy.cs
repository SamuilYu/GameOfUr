using System.Collections.Generic;
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
			return stationedSoldiers[FilledCapacity].Team != soldier.Team;
		}

		public virtual void Receive(Soldier soldier)
		{
			GD.Print("Received");
			if (FilledCapacity >= MaxCapacity) return;
			stationedSoldiers.Add(soldier);
			FilledCapacity++;
			soldier.CurrentTile = GetParent<Tile>();
			soldier.ZIndex = FilledCapacity;
			soldier.GetParent<Squad>()._On_Dice_Roll(3);
		}
	}
}
