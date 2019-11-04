using Platformer.Scripts.Characters.StateMachine;
using UnityEngine;

class SpecialCommand : ICommand
{
    public void Execute(GameObject obj)
    {
        StateMachine sm = obj.GetComponent<StateMachine>();
        sm.HandleInput(Commands.Special);
    }
}
