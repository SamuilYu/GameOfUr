using Godot;

namespace royalgameofur
{
    public class Squad : Node2D
    {
        [Export()]
        public PlayerTeam Team { get; private set; }
        
    }
}