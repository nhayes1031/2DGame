using UnityEngine;

namespace Platformer.Scripts.Characters.Enemies.Abmoor
{
    public class AbmoorAIInput : IInput
    {
        private Vector2 CalculateWalk()
        {
            Vector2 walk = new Vector2(1, 0);
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

    }
}
