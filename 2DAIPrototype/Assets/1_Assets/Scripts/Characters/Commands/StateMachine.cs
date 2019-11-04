using UnityEngine;

namespace Platformer.Scripts.Characters.StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        public State state;
        public State remainState;

        //This is the update loop
        public void HandleInput(Commands command)
        {
            State newState = state.HandleCommand(gameObject, command);
            if (newState != null && newState != remainState)
            {
                state.OnExit();
                state = newState;
                state.OnEnter();
            }
        }
    }
}
