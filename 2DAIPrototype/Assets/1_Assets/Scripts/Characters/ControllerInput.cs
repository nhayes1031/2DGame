using UnityEngine;

namespace Platformer.Scripts.Characters
{
    public class ControllerInput : IInput
    {
        private Vector2 CalculateLook()
        {
            Vector2 look = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            if (look == Vector2.zero)
            {
                return Look;
            }
            return look;
        }

        public void ReadInput()
        {
            Walk = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxisRaw("Vertical"));
            Look = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            JumpReleased = Input.GetButtonUp("Jump");
            JumpPressed = Input.GetButtonDown("Jump");
            PossessionPressed = Input.GetButtonDown("Left Click");
            DashPressed = Input.GetButtonDown("Fire3");
        }

        public Vector2 Walk { get; private set; }
        public Vector2 Look { get; private set; }
        public bool JumpReleased { get; private set; }
        public bool JumpPressed { get; private set; }
        public bool PossessionPressed { get; private set; }
        public bool DashPressed { get; private set; }
    }
}
