using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "AI2/Decisions/LedgeCheck")]
    public class LedgeCondition : Condition
    {
        [SerializeField]
        public LayerMask collisionMask;

        public override bool CheckCondition(StateManager state)
        {
            return Look(state);
        }

        private bool Look(StateManager state)
        {
            bool shouldJump = false;

            Vector2 rayOriginTop = state.coll.collisions.faceDir == 1 ? state.coll.raycastOrigins.topRight : state.coll.raycastOrigins.topLeft;
            Vector2 rayOriginBottom = state.coll.collisions.faceDir == 1 ? state.coll.raycastOrigins.bottomRight : state.coll.raycastOrigins.bottomLeft;

            RaycastHit2D ledgeCheck = Physics2D.Raycast(rayOriginTop, new Vector2(state.coll.collisions.faceDir * 1.5f, 2), 2.5f, collisionMask);
            RaycastHit2D wallCheck = Physics2D.Raycast(rayOriginBottom, Vector2.right * state.coll.collisions.faceDir, 1.5f, collisionMask);
            Debug.DrawRay(rayOriginTop, new Vector2(state.coll.collisions.faceDir * 1.5f, 2), Color.red);
            Debug.DrawRay(rayOriginBottom, Vector2.right * state.coll.collisions.faceDir * 1.5f, Color.red);
            shouldJump = !ledgeCheck && wallCheck;

            return shouldJump;
        }
    }
}
