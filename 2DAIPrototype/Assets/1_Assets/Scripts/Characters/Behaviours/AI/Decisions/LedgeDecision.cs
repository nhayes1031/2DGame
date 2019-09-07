using Platformer.Scripts.Characters.Behaviours;
using UnityEngine;

namespace Platformer.Scripts.Characters.AI
{
    [CreateAssetMenu(menuName = "AI/Decisions/LedgeCheck")]
    public class LedgeDecision : Decision
    {
        [SerializeField]
        public LayerMask collisionMask;

        public override bool Decide(AIControllerBehaviour controller)
        {
            return Look(controller);
        }

        private bool Look(AIControllerBehaviour controller)
        {
            bool shouldJump = false;

            Vector2 rayOrigin = controller.coll.collisions.faceDir == 1 ? controller.coll.raycastOrigins.topRight : controller.coll.raycastOrigins.topLeft;

            RaycastHit2D ledgeCheck = Physics2D.Raycast(rayOrigin, new Vector2(controller.coll.collisions.faceDir * 1.5f, 2), 2.5f, collisionMask);
            RaycastHit2D wallCheck = Physics2D.Raycast(rayOrigin, Vector2.right * controller.coll.collisions.faceDir, 1.5f, collisionMask);
            Debug.DrawRay(rayOrigin, new Vector2(controller.coll.collisions.faceDir * 1.5f, 2), Color.red);
            Debug.DrawRay(rayOrigin, Vector2.right * controller.coll.collisions.faceDir * 1.5f, Color.red);
            shouldJump = !ledgeCheck && wallCheck;

            return shouldJump;
        }
    }
}
