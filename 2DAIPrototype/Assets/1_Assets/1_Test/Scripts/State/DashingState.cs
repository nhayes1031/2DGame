using System.Collections;
using UnityEngine;

namespace Assets._1_Assets._1_Test.Scripts.State
{
    class DashingState : State
    {
        private bool dashCompleted = false;
        private bool isDashing = false;

        private Vector2 direction = Vector2.zero;

        public DashingState()
        {
            direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
            if (direction == Vector2.zero)
                direction = new Vector2(1, 0);
        }

        public State Update(PhysicsObject po, PlayerData playerData)
        {
            if (playerData.CurrentDashes >= playerData.AllowedDashes)
                return TransitionCheck(po);

            Animator animator = po.GetComponent<Animator>();

            po.SetVelocity(direction * playerData.DashSpeed);
            if (!isDashing)
            {
                isDashing = true;
                po.StartCoroutine(CompleteDash(playerData));
                if (animator != null)
                    animator.SetBool("IsDashing", true);
            }

            if (dashCompleted)
            {
                playerData.IncrementCurrentDashes();
                if (animator != null)
                    animator.SetBool("IsDashing", false);
                return TransitionCheck(po);
            }
            return this;
        }

        private IEnumerator CompleteDash(PlayerData playerData)
        {
            yield return new WaitForSeconds(playerData.DashDuration);

            dashCompleted = true;
        }

        private State TransitionCheck(PhysicsObject po)
        {
            if (Input.GetButtonDown("Jump"))
                return new JumpingState();
            if (!po.Grounded)
                return new FallingState();
            else
                return new RunningState();
        }
    }
}
