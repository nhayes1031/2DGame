using Platformer.Scripts.Characters.Behaviours;
using UnityEngine;

namespace Platformer.Scripts.Characters.AI.Newcode
{
    [CreateAssetMenu(menuName = "AI/State2")]
    public class State : ScriptableObject
    {
        public Action[] actions;
        public Transition[] transitions;

        public void UpdateState(AiInputHandler controller)
        {
            DoActions(controller);
            CheckTransitions(controller);
        }

        private void DoActions(AiInputHandler controller)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i].Act(controller);
            }
        }

        private void CheckTransitions(AiInputHandler controller)
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