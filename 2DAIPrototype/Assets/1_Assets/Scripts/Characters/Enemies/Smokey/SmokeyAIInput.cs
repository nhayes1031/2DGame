using UnityEngine;

namespace Platformer.Scripts.Characters.Enemies.Smokey
{
    public class SmokeyAIInput : IInput
    {
        SmokeyRaycastController controller;
        private Direction movementDirection = Direction.right;

        float jumpTimerDelay = 0.5f;
        float jumpTimer;
        private Vector2 spawnPosition;

        public SmokeyAIInput(SmokeyRaycastController controller)
        {
            this.controller = controller;
        }

        public void Init(Vector2 position)
        {
            spawnPosition = position;
        }

        private Vector2 CalculateWalk()
        {
            Vector2 walk = new Vector2(1, 0);
            int edgeSide = controller.OnEdge();
            switch (edgeSide)
            {
                case 1:
                    movementDirection = Direction.left;
                    break;
                case -1:
                    movementDirection = Direction.right;
                    break;
                default:
                    break;
            };

            walk *= (int)movementDirection;
            return walk;
        }

        private bool CalculateJump()
        {
            if (jumpTimer < Time.realtimeSinceStartup)
            {
                if (controller.ShouldIJump())
                {
                    jumpTimer = Time.realtimeSinceStartup + jumpTimerDelay;
                    return true;
                }
            }
            return false;
        }

        public void ReadInput()
        {
            Walk = CalculateWalk();
            Look = Vector2.zero;
            JumpReleased = false;
            JumpPressed = CalculateJump();
            PossessionPressed = false;
            DashPressed = false;
        }

        public Vector2 Walk { get; private set; }
        public Vector2 Look { get; private set; }
        public bool JumpReleased { get; private set; }
        public bool JumpPressed { get; private set; }
        public bool PossessionPressed { get; private set; }
        public bool DashPressed { get; private set; }

        public enum Direction
        {
            left = -1,
            right = 1,
        };
    }
}
