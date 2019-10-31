using MyBox;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "AI2/Actions/Patrol")]
    public class PatrolAction : StateActions
    {
        [SerializeField]
        [Range(0, 1)]
        public float speed;
        [SerializeField]
        public bool checkForLedge = false;
        [SerializeField]
        public bool territorial = false;
        [ConditionalField("territorial")]
        public float roamDistance = 100;
        [SerializeField]
        public LayerMask collisionMask;

        RightCommand rightCom = new RightCommand();
        LeftCommand leftCom = new LeftCommand();
        ICommand command;

        private int direction = 1;

        public override void Execute(StateManager states)
        {
            CheckDirection(states);
            command = direction == 1 ? (ICommand)rightCom : (ICommand)leftCom;
            Patrol(states);
        }

        private void CheckDirection(StateManager states)
        {
            CheckForWallCollisions(states);
            if (checkForLedge)
            {
                CheckForLedge(states);
            }
            if (territorial)
            {
                CheckForMaximumRoamDistance(states);
            }
        }

        private void CheckForWallCollisions(StateManager states)
        {
            if (states.coll.collisions.right || states.coll.collisions.left)
            {
                FlipDirection(states);
            }
        }

        private void CheckForLedge(StateManager states)
        {
            Vector2 rayOrigin = states.coll.collisions.faceDir == 1 ? states.coll.raycastOrigins.bottomRight : states.coll.raycastOrigins.bottomLeft;
            rayOrigin.x += states.coll.collisions.faceDir * 0.25f;

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, 0.5f, collisionMask);
            Debug.DrawRay(rayOrigin, Vector2.down * 0.25f);
            if (hit.collider == null)
            {
                FlipDirection(states);
            }
        }

        private void CheckForMaximumRoamDistance(StateManager states)
        {
            Vector2 leftMost = states.origin;
            leftMost.x -= roamDistance;
            Vector2 rightMost = states.origin;
            rightMost.x += roamDistance;
            Debug.DrawRay(leftMost, Vector2.down, Color.green, 10);
            Debug.DrawRay(rightMost, Vector2.down, Color.green, 10);

            if (Vector2.Distance(states.gameObject.transform.position, states.origin) >= roamDistance)
            {
                direction = IsLeft(states.gameObject.transform.position, states.origin) ? 1 : -1;
            }
        }

        private bool IsLeft(Vector2 a, Vector2 b)
        {
            return -a.x * b.y + a.y * b.x < 0;
        }

        private void FlipDirection(StateManager states)
        {
            direction = states.coll.collisions.faceDir * -1;
        }

        private void Patrol(StateManager controller)
        {
            command.Execute(controller.gameObject);
        }
    }
}
