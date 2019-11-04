using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "AI/Actions/Chase")]
    public class ChaseAction : StateActions
    {
        public bool checkForLedge = false;
        public float chaseDistance = 100;
        public LayerMask collisionMask;

        private int direction = 1;
        private RightCommand rightCom = new RightCommand();
        private LeftCommand leftCom = new LeftCommand();
        private ICommand command;

        public override void Execute(StateManager states)
        {
            CheckDirection(states);
            command = direction == 1 ? (ICommand)rightCom : (ICommand)leftCom;
            Chase(states);
        }

        private void CheckDirection(StateManager states)
        {
            CheckDirectionOfTarget(states);
        }

        private void CheckDirectionOfTarget(StateManager states)
        {
            direction = IsLeft(states.gameObject.transform.position, states.target.transform.position) ? -1 : 1;
        }

        private bool IsLeft(Vector2 a, Vector2 b)
        {
            return -a.x * b.y + a.y * b.x < 0;
        }

        private void Chase(StateManager states)
        {
            command.Execute(states.gameObject);
        }
    }
}
