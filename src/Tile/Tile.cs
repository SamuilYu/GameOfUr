using Godot;

namespace royalgameofur
{
    public class Tile: Node2D
    {
        private TileType type;
        private Button button;

        public override void _Ready()
        {
            type = GetNode("Type") as TileType;
            button = GetNode("TileButton") as Button;

            if (button == null) return;
            button.Connect("pressed", this, "_On_Pressed");
            button.Icon = type.Texture;
        }

        public void _On_Pressed()
        {
            GD.Print("I have been pressed");
        }
        
    }
    

}