using UnityEngine;

namespace Platformer.Scripts.Characters.Enemies.Spotmini
{
    public class SpotminiMovement
    {
        private readonly IInput spotminiInput;
        private readonly Transform transformToMove;
        private readonly SpotminiSettings spotminiSettings;
        private readonly SpotminiRaycastController spotminiRaycastController;

        private Vector2 velocity;
        private float velocityXSmoothing;

        public SpotminiMovement(IInput spotminiInput, Transform transformToMove, SpotminiSettings spotminiSettings, SpotminiRaycastController spotminiRaycastController)
        {
            this.spotminiInput = spotminiInput;
            this.transformToMove = transformToMove;
            this.spotminiSettings = spotminiSettings;
            this.spotminiRaycastController = spotminiRaycastController;
        }

        public void Tick()
        {
            CalculateVelocity();

            Vector2 adjustedMoveAmount = spotminiRaycastController.Move(velocity * Time.deltaTime);
            transformToMove.Translate(adjustedMoveAmount);

            if (spotminiRaycastController.collisions.above || spotminiRaycastController.collisions.below)
            {
                velocity.y = 0;
            }
        }

        public void OnJumpInputDown()
        {
            if (spotminiRaycastController.collisions.below)
            {
                velocity.y = spotminiSettings.MaxJumpVelocity;
            }
        }

        private void CalculateVelocity()
        {
            float targetVelocityX = spotminiInput.Walk.x * spotminiSettings.MoveSpeed;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (spotminiRaycastController.collisions.below) ? spotminiSettings.AccelerationTimeGrounded : spotminiSettings.AccelerationTimeAirborne);
            velocity.y += spotminiSettings.Gravity * Time.deltaTime;
        }
    }
}
