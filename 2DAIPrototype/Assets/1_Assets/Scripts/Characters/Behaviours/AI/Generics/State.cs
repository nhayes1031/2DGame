using Platformer.Scripts.Characters.Behaviours;
using UnityEngine;

namespace Platformer.Scripts.Characters.AI
{
    [CreateAssetMenu(menuName = "AI/State")]
    public class State : ScriptableObject
    {
        public Action[] actions;
        public Transition[] transitions;

        public void UpdateState(AIControllerBehaviour controller)
        {
            DoActions(controller);
            CheckTransitions(controller);
        }

        private void DoActions(AIControllerBehaviour controller)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i].Act(controller);
            }
        }

        private void CheckTransitions(AIControllerBehaviour controller)
        {
            for (int i = 0; i < transitions.Length; i++)
            {
                bool decisionSucceeded = transitions[i].decision.Decide(controller);

                if (decisionSucceeded)
                {
                    controller.TransitionToState(transitions[i].trueState);
                } else
                {
                    controller.TransitionToState(transitions[i].falseState);
                }
            }
        }
    }
}