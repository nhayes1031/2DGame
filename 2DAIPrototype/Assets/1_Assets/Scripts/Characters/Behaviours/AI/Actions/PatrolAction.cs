using Platformer.Scripts.Characters.Behaviours;
using UnityEngine;

namespace Platformer.Scripts.Characters.AI
{
    [CreateAssetMenu(menuName = "AI/Actions/Patrol")]
    public class PatrolAction : Action
    {
        [SerializeField]
        [Range(0, 1)]
        public float speed;
        [SerializeField]
        public bool checkForLedge = false;
        [SerializeField]
        public LayerMask collisionMask;

        int direction = 1;

        public override void Act(AIControllerBehaviour controller)
        {
            CheckDirection(controller);
            Patrol(controller);
        }

        private void CheckDirection(AIControllerBehaviour controller)
        {
            if (controller.coll.collisions.right || controller.coll.collisions.left)
            {
                direction = controller.coll.collisions.faceDir * -1;
                return;
            }

            if (checkForLedge)
            {
                Vector2 rayOrigin = controller.coll.collisions.faceDir == 1 ? controller.coll.raycastOrigins.bottomRight : controller.coll.raycastOrigins.bottomLeft;
                rayOrigin.x += controller.coll.collisions.faceDir * 0.25f;

                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, 0.5f, collisionMask);
                Debug.DrawRay(rayOrigin, Vector2.down * 0.25f);
                if (hit.collider == null)
                {
                    direction = controller.coll.collisions.faceDir * -1;
                }
            }
        }

        private void Patrol(AIControllerBehaviour controller)
        {
            Vector2 move = Vector2.zero;
            move.x = speed * direction;
            controller.TriggerOnMove(move);
        }
    }
}
