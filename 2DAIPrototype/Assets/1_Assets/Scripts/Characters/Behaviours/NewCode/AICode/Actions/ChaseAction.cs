using MyBox;
using Platformer.Scripts.Characters.Behaviours;
using UnityEngine;

namespace Platformer.Scripts.Characters.AI.Newcode
{
    [CreateAssetMenu(menuName = "AI/Actions/Chase")]
    public class ChaseAction : Action
    {
        public bool checkForLedge = false;
        public float chaseDistance = 100;
        public LayerMask collisionMask;

        private int direction = 1;
        private RightCommand rightCom = new RightCommand();
        private LeftCommand leftCom = new LeftCommand();
        private ICommand command;

        public override void Act(AiInputHandler controller)
        {
            CheckDirection(controller);
            command = direction == 1 ? (ICommand)rightCom : (ICommand)leftCom;
            Chase(controller);
        }

        private void CheckDirection(AiInputHandler controller)
        {
            // CheckForMaximumChaseDistance(controller);
            CheckDirectionOfTarget(controller);
        }

        private void CheckDirectionOfTarget(AiInputHandler controller)
        {
                direction = IsLeft(controller.gameObject.transform.position, controller.target.transform.position) ? -1 : 1;
        }

        private bool IsLeft(Vector2 a, Vector2 b)
        {
            return -a.x * b.y + a.y * b.x < 0;
        }

        private void Chase(AiInputHandler controller)
        {
            command.Execute(controller.gameObject);
        }
    }
}
