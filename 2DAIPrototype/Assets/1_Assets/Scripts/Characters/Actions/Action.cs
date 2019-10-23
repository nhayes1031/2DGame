using UnityEngine;

namespace Platformer.Scripts.Characters.StateMachine
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Act(GameObject obj);
        public abstract void OnEnter();
        public abstract void OnExit();
    }
}
