using UnityEngine;

namespace Assets._1_Assets._1_Test.Scripts.State
{
    class RunningState : State
    {
        public State Update(PhysicsObject po, PlayerData playerData)
        {
            Vector2 move = Vector2.zero;
            move.x = Input.GetAxis("Horizontal");
            po.SetVelocity(move * playerData.MaxSpeed);

            Animator animator = po.GetComponent<Animator>();
            if (animator != null)
                animator.SetFloat("Magnitude", po.TargetVelocity.magnitude);

            if (Input.GetButtonDown("Jump"))
                return new JumpingState();
            if (Input.GetButtonDown("Fire3"))
                return new DashingState();
            if (!po.Grounded)
                return new FallingState();
            return this;
        }
    }
}
