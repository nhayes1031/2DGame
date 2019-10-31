using UnityEngine;

namespace SA
{
    public abstract class Condition : ScriptableObject
    {
		public string description;

        public abstract bool CheckCondition(StateManager state);

    }
}
