using System.Collections;
using UnityEngine;

namespace Platformer.Scripts.Characters.Nanoobs
{
    public class NanoobMovement
    {
        private readonly IInput nanoobInput;
        private readonly Transform transformToMove;
        private readonly NanoobSettings nanoobSettings;
        private readonly NanoobRaycastController nanoobRaycastController;

        private Vector2 velocity;
        private float velocityXSmoothing;
        private bool hasJumpedTwice;
        private int wallDirX;
        private bool wallSliding;
        private float timeToWallUnstick;

        public NanoobMovement(IInput nanoobInput, Transform transformToMove, NanoobSettings nanoobSettings, NanoobRaycastController nanoobRaycastController)
        {
            this.nanoobInput = nanoobInput;
            this.transformToMove = transformToMove;
            this.nanoobSettings = nanoobSettings;
            this.nanoobRaycastController = nanoobRaycastController;
        }

        public void Tick()
        {
            CalculateVelocity();
            HandleWallSliding();

            Vector2 adjustedMoveAmount = nanoobRaycastController.Move(velocity * Time.deltaTime);
            transformToMove.Translate(adjustedMoveAmount);

            if (nanoobRaycastController.collisions.above || nanoobRaycastController.collisions.below)
            {
                velocity.y = 0;
            }

            if (nanoobRaycastController.collisions.below)
            {
                hasJumpedTwice = false;
            }
        }

        private void CalculateVelocity()
        {
            float targetVelocityX = nanoobInput.Walk.x * nanoobSettings.MoveSpeed;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (nanoobRaycastController.collisions.below) ? nanoobSettings.AccelerationTimeGrounded : nanoobSettings.AccelerationTimeAirborne);
            velocity.y += nanoobSettings.Gravity * Time.deltaTime;
        }

        public void OnJumpInputDown()
        {
            if (wallSliding)
            {
                if (wallDirX == nanoobInput.Walk.x)
                {
                    velocity.x = -wallDirX * nanoobSettings.WallJumpClimb.x;
                    velocity.y = nanoobSettings.WallJumpClimb.y;
                }
                else if (nanoobInput.Walk.x == 0)
                {
                    velocity.x = -wallDirX * nanoobSettings.WallJumpOff.x;
                    velocity.y = nanoobSettings.WallJumpOff.y;
                }
                else
                {
                    velocity.x = -wallDirX * nanoobSettings.WallLeap.x;
                    velocity.y = nanoobSettings.WallLeap.y;
                }
            }
            if (nanoobRaycastController.collisions.below)
            {
                velocity.y = nanoobSettings.MaxJumpVelocity;
            }
            else
            {
                if (!hasJumpedTwice)
                {
                    velocity.y = nanoobSettings.MaxJumpVelocity;
                    hasJumpedTwice = true;
                }
            }
        }

        public void OnJumpInputUp()
        {
            if (velocity.y > nanoobSettings.MinJumpVelocity)
            {
                velocity.y = nanoobSettings.MinJumpVelocity;
            }
        }

        void HandleWallSliding()
        {
            wallDirX = (nanoobRaycastController.collisions.left) ? -1 : 1;
            wallSliding = false;
            if ((nanoobRaycastController.collisions.left || nanoobRaycastController.collisions.right) && !nanoobRaycastController.collisions.below && velocity.y < 0)
            {
                wallSliding = true;

                if (velocity.y < -nanoobSettings.WallSlideSpeedMax)
                {
                    velocity.y = -nanoobSettings.WallSlideSpeedMax;
                }

                if (timeToWallUnstick > 0)
                {
                    velocityXSmoothing = 0;
                    velocity.x = 0;

                    if (nanoobInput.Walk.x != wallDirX && nanoobInput.Walk.x != 0)
                    {
                        timeToWallUnstick -= Time.deltaTime;
                    }
                    else
                    {
                        timeToWallUnstick = nanoobSettings.WallStickTime;
                    }
                }
                else
                {
                    timeToWallUnstick = nanoobSettings.WallStickTime;
                }

            }

        }
    }
}
