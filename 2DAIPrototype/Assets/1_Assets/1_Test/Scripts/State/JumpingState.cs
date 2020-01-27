using UnityEngine;

namespace Assets._1_Assets._1_Test.Scripts.State
{
    class JumpingState : State
    {
        public State Update(PhysicsObject po, PlayerData playerData)
        {
            if (playerData.CurrentJumps < playerData.AllowedJumps)
            {
                playerData.IncrementCurrentJumps();

                po.SetVelocity(new Vector2(0, playerData.JumpSpeed));

                Animator animator = po.GetComponent<Animator>();
                if (animator != null)
                    animator.SetTrigger("Jump");
            }

            if (Input.GetButtonDown("Fire3"))
                return new DashingState();
            if (!po.Grounded)
                return new FallingState();
            else
                return new RunningState();
        }
    }
}
