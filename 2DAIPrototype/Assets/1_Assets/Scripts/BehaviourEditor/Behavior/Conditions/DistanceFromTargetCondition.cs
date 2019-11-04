using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "AI/Decisions/DistanceFromTarget")]
    public class DistanceFromTargetCondition : Condition
    {
        public float distance;
        public Equality equality = Equality.greaterThan;

        public override bool CheckCondition(StateManager state)
        {
            if (equality == Equality.greaterThan) return Vector2.Distance(state.gameObject.transform.position, state.target.gameObject.transform.position) > distance;

            return Vector2.Distance(state.gameObject.transform.position, state.target.gameObject.transform.position) < distance;
        }

        public enum Equality
        {
            greaterThan, lessThan
        };
    }
}
