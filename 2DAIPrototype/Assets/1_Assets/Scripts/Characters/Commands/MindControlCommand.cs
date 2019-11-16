using Platformer.Scripts.Characters.StateMachine;
using UnityEngine;

public class MindControlCommand : ICommand
{
    public void Execute(GameObject obj)
    {
        StateMachine sm = obj.GetComponent<StateMachine>();
        sm.HandleInput(Commands.MindControl);
    }
}
