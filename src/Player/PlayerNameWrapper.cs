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

        public string GetPossessive()
        {
            if (name != null && name.EndsWith("s"))
            {
                return name + "'";
            }
            
            return name + "'s";
        }

        public string GetPlayerName()
        {
            return name;
        }
    }
}