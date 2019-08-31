using UnityEngine;

namespace Platformer.Scripts.Characters.Enemies.Abmoor
{
    public class AbmoorMovement
    {
        private readonly IInput abmoorInput;
        private readonly Transform transformToMove;
        private readonly AbmoorSettings abmoorSettings;
        private readonly AbmoorRaycastController abmoorRaycastController;

        private Vector2 velocity;
        private float velocityXSmoothing;
        private float velocityYSmoothing;

        public AbmoorMovement(IInput abmoorInput, Transform transformToMove, AbmoorSettings abmoorSettings, AbmoorRaycastController abmoorRaycastController)
        {
            this.abmoorInput = abmoorInput;
            this.transformToMove = transformToMove;
            this.abmoorSettings = abmoorSettings;
            this.abmoorRaycastController = abmoorRaycastController;
        }

        public void Tick()
        {
            CalculateVelocity();

            Vector2 adjustedMoveAmount = abmoorRaycastController.Move(velocity * Time.deltaTime);
            transformToMove.Translate(adjustedMoveAmount);
        }

        void CalculateVelocity()
        {
            float targetVelocity = abmoorInput.Walk.x * abmoorSettings.MoveSpeed;
            switch (abmoorRaycastController.stickyDirection)
            {
                case AbmoorRaycastController.Direction.up:
                    velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocity, ref velocityXSmoothing, 0);
                    velocity.y = 0;
                    break;
                case AbmoorRaycastController.Direction.right:
                    velocity.x = Mathf.SmoothDamp(velocity.y, targetVelocity, ref velocityYSmoothing, 0);
                    velocity.y = 0;
                    break;
                case AbmoorRaycastController.Direction.down:
                    velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocity, ref velocityXSmoothing, 0);
                    velocity.y = 0;
                    break;
                case AbmoorRaycastController.Direction.left:
                    velocity.x = Mathf.SmoothDamp(velocity.y, targetVelocity, ref velocityYSmoothing, 0);
                    velocity.y = 0;
                    break;
            }
        }
    }
}
