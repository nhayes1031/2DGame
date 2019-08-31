using UnityEngine;

namespace Platformer.Scripts.Characters.Enemies.Smokey
{
    public class SmokeyMovement
    {
        private readonly IInput smokeyAIInput;
        private readonly Transform transformToMove;
        private readonly SmokeySettings smokeySettings;
        private readonly SmokeyRaycastController smokeyRaycastController;

        private Vector2 velocity;
        private float velocityXSmoothing;

        public SmokeyMovement(IInput smokeyAIInput, Transform transformToMove, SmokeySettings smokeySettings, SmokeyRaycastController smokeyRaycastController)
        {
            this.smokeyAIInput = smokeyAIInput;
            this.transformToMove = transformToMove;
            this.smokeySettings = smokeySettings;
            this.smokeyRaycastController = smokeyRaycastController;
        }

        public void Tick()
        {
            CalculateVelocity();

            Vector2 adjustedMoveAmount = smokeyRaycastController.Move(velocity * Time.deltaTime);
            transformToMove.Translate(adjustedMoveAmount);

            if (smokeyRaycastController.collisions.above || smokeyRaycastController.collisions.below)
            {
                velocity.y = 0;
            }
        }

        public void OnJumpInputDown()
        {
            if (smokeyRaycastController.collisions.below)
            {
                velocity.y = smokeySettings.MaxJumpVelocity;
            }
        }

        private void CalculateVelocity()
        {
            float targetVelocityX = smokeyAIInput.Walk.x * smokeySettings.MoveSpeed;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (smokeyRaycastController.collisions.below) ? smokeySettings.AccelerationTimeGrounded : smokeySettings.AccelerationTimeAirborne);
            velocity.y += smokeySettings.Gravity * Time.deltaTime;
        }
    }
}
