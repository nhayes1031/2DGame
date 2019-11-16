using Platformer.Scripts.Characters.StateMachine;
using UnityEngine;

public class AltSpecialCommand : ICommand
{
    public void Execute(GameObject obj)
    {
        StateMachine sm = obj.GetComponent<StateMachine>();
        sm.HandleInput(Commands.AltSpecial);
    }
}
