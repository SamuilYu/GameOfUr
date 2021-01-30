using System;
using Godot;

namespace royalgameofur
{

    public class NameLineEdit : LineEdit
    {
        [Signal]
        public delegate void EmptyName(int playerNumber, string name);
        
        [Signal]
        public delegate void NonEmptyName(int playerNumber, string name);
        
        public void NameChanged(string name)
        {

            if (GetParent().Name.Contains("Black"))
            {
                EmitSignal(name == "" ? "EmptyName" : "NonEmptyName", 1, name);
                GetNode<PlayerNameWrapper>("/root/Player1NameWrapper").SetPlayerName(name);
            }
            else
            {
                EmitSignal(name == "" ? "EmptyName" : "NonEmptyName", 2, name);
                GetNode<PlayerNameWrapper>("/root/Player2NameWrapper").SetPlayerName(name);
            }
        }
    }
}