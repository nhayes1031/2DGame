using UnityEngine;

namespace Assets._1_Assets._1_Test.Scripts.State
{
    class FallingState : State
    {
        public State Update(PhysicsObject po, PlayerData playerData)
        {
            Vector2 move = Vector2.zero;
            move.x = Input.GetAxis("Horizontal");
            po.SetVelocity(move * playerData.FallingMaxSpeed);

            Animator animator = po.GetComponent<Animator>();
            if (animator != null)
                animator.SetBool("IsFalling", true);

            if (Input.GetButtonDown("Jump"))
                return new JumpingState();
            if (Input.GetButtonDown("Fire3"))
                return new DashingState();
            if (po.Grounded)
            {
                animator.SetBool("IsFalling", false);
                return new RunningState();
            }
            return this;
        }
    }
}
