using Godot;

namespace royalgameofur
{
	public class TileType: Node2D
	{
		public Texture Texture { get; protected set; }

		public int MaxCapacity { get; protected set; }
	}
}
