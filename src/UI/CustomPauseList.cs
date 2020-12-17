using Godot;

namespace royalgameofur
{
    public class CustomPauseList : VBoxContainer
    {
        
        [Signal]
        delegate void Continue(); 
        
        [Signal]
        delegate void Replay(); 

        [Signal]
        delegate void Rules(); 

        [Signal]
        delegate void Settings(); 
    
        [Signal]
        delegate void ToMainMenu();

        [Signal]
        delegate void Quit();

        private void ContinuePressed()
        {
            EmitSignal("Continue");
        }
        
        private void ReplayPressed()
        {
            EmitSignal("Replay");
        }
    
        private void RulesPressed()
        {
            EmitSignal("Rules");
        }

        private void SettingsPressed()
        {
            EmitSignal("Settings");
        }
    
        private void ToMainMenuPressed()
        {
            EmitSignal("ToMainMenu");
        }

        private void QuitPressed()
        {
            EmitSignal("Quit");
        }
    }
}