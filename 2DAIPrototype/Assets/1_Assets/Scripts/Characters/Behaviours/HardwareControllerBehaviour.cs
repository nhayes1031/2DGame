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
        public event Action OnAttack;

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
            CheckAttack();
        }

        private void CheckMove()
        {
            Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            OnMove?.Invoke(move);
        }

        private void CheckLook()
        {
            Vector2 look = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            OnLook?.Invoke(look);
        }

        private void CheckJump()
        {
            if (Input.GetButtonDown("Jump"))
            {
                OnJump?.Invoke();
            }
        }

        private void CheckJumpReleased()
        {
            if (Input.GetButtonUp("Jump"))
            {
                OnJumpReleased?.Invoke();
            }
        }

        private void CheckAttack()
        {
            if (Input.GetButtonDown("Left Click"))
            {
                OnAttack?.Invoke();
            }
        }
    }
}
