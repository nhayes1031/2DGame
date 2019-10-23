using UnityEngine;

namespace Platformer.Scripts.Characters.StateMachine
{
    [CreateAssetMenu(menuName = "Character/StateMachine/State")]
    public class State : ScriptableObject
    {
        public Action action;
        public Transition[] transitions;

        public State HandleCommand(GameObject obj, Commands command)
        {
            action?.Act(obj);

            foreach (Transition trans in transitions)
            {
                if (command == trans.reason)
                {
                    return trans.trueState;
                }
            }
            return null;
        }

        public void OnEnter()
        {
            action.OnEnter();
        }

        public void OnExit()
        {
            action.OnExit();
        }
    }
}
