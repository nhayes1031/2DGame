using UnityEngine;

namespace Platformer.Scripts.Characters.Enemies.Roomba
{
    public class RoombaAIInput : IInput
    {
        RoombaRaycastController controller;
        private Direction movementDirection = Direction.right;

        public RoombaAIInput(RoombaRaycastController controller)
        {
            this.controller = controller;
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

        public void ReadInput()
        {
            Walk = CalculateWalk();
            Look = Vector2.zero;
            JumpReleased = false;
            JumpPressed = false;
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
