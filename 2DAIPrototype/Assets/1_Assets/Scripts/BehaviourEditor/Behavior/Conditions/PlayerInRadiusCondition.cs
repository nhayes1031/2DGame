using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "AI2/Decisions/PlayerInRadius")]
    public class PlayerInRadiusCondition : Condition
    {
        public float radius;

        public override bool CheckCondition(StateManager state)
        {
            Collider2D[] collisions = Physics2D.OverlapCircleAll(state.transform.position, radius, LayerMask.GetMask("Pushbox"));

            foreach (Collider2D collision in collisions)
            {
                if (collision.tag == "Player")
                {
                    state.SetNewTarget(collision.gameObject);
                    return true;
                }
            }

            return false;
        }
    }
}
