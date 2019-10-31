using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "AI2/Decisions/GroundedCheck")]
    public class GroundedCondition : Condition
    {
        public override bool CheckCondition(StateManager state)
        {
            return state.coll.collisions.below;
        }
    }
}
