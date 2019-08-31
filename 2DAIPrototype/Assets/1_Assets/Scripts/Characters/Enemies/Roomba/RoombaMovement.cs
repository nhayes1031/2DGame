using UnityEngine;

namespace Platformer.Scripts.Characters.Enemies.Roomba
{
    public class RoombaMovement
    {
        private readonly IInput roombaInput;
        private readonly Transform transformToMove;
        private readonly RoombaSettings roombaSettings;
        private readonly RoombaRaycastController roombaRaycastController;

        private Vector2 velocity;
        private float velocityXSmoothing;

        public RoombaMovement(IInput roombaInput, Transform transformToMove, RoombaSettings roombaSettings, RoombaRaycastController roombaRaycastController)
        {
            this.roombaInput = roombaInput;
            this.transformToMove = transformToMove;
            this.roombaSettings = roombaSettings;
            this.roombaRaycastController = roombaRaycastController;
        }

        public void Tick()
        {
            CalculateVelocity();

            Vector2 adjustedMoveAmount = roombaRaycastController.Move(velocity * Time.deltaTime);
            transformToMove.Translate(adjustedMoveAmount);

            if (roombaRaycastController.collisions.above || roombaRaycastController.collisions.below)
            {
                velocity.y = 0;
            }
        }

        private void CalculateVelocity()
        {
            float targetVelocityX = roombaInput.Walk.x * roombaSettings.MoveSpeed;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (roombaRaycastController.collisions.below) ? roombaSettings.AccelerationTimeGrounded : roombaSettings.AccelerationTimeAirborne);
            velocity.y += roombaSettings.Gravity * Time.deltaTime;
        }
    }
}
