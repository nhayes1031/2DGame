using UnityEngine;

namespace Platformer.Scripts.Characters.StateMachine
{
    [System.Serializable]
    public class Transition
    {
        public Commands reason;
        public State trueState;
        public State falseState;
    }
}
