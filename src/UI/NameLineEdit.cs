using System;
using Godot;

namespace royalgameofur
{

    public class NameLineEdit : LineEdit
    {
        [Signal]
        public delegate void EmptyName(int playerNumber);
        
        [Signal]
        public delegate void NonEmptyName(int playerNumber);
        
        public void NameChanged(string name)
        {

            if (GetParent().Name.Contains("Black"))
            {
                EmitSignal(name == "" ? "EmptyName" : "NonEmptyName", 1);
                GetNode<PlayerNameWrapper>("/root/Player1NameWrapper").SetPlayerName(name);
            }
            else
            {
                EmitSignal(name == "" ? "EmptyName" : "NonEmptyName", 2);
                GetNode<PlayerNameWrapper>("/root/Player2NameWrapper").SetPlayerName(name);
            }
        }
    }
}