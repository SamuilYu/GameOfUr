using System;
using Godot;

namespace royalgameofur
{
    public class PlayerNameWrapper : Node
    {
        private string name;

        public void SetPlayerName(string name)
        {
            this.name = name;
        }
    }
}