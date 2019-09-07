using System;
using UnityEngine;

namespace Platformer.Scripts.Characters.Behaviours
{
    public class HardwareControllerBehaviour : MonoBehaviour, IController
    {
        public event Action<Vector2> OnMove;
        public event Action<Vector2> OnLook;
        public event Action OnJump;
        public event Action OnJumpReleased;

        private void Update()
        {
            InputHandler();
        }

        void InputHandler()
        {
            CheckMove();
            CheckLook();
            CheckJump();
            CheckJumpReleased();
        }

        private void CheckMove()
        {
            Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (OnMove != null)
            {
                OnMove(move);
            }
        }

        private void CheckLook()
        {
            Vector2 look = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            if (OnLook != null)
            {
                OnLook(look);
            }
        }

        private void CheckJump()
        {
            if (Input.GetButtonDown("Jump") && OnJump != null)
            {
                OnJump();
            }
        }

        private void CheckJumpReleased()
        {
            if (Input.GetButtonUp("Jump") && OnJumpReleased != null)
            {
                OnJumpReleased();
            }
        }
    }
}
