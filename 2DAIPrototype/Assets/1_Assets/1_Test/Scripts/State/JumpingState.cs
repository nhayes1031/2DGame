using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._1_Assets._1_Test.Scripts.State
{
    class JumpingState : State
    {
        private float jumpSpeed = 10f;

        public State Update(PhysicsObject po)
        {
            po.targetVelocity = new Vector2(0, jumpSpeed);

            Animator animator = po.GetComponent<Animator>();
            if (animator != null)
                animator.SetTrigger("Jump");

            if (Input.GetButtonDown("Fire3"))
                return new DashingState();
            if (!po.grounded)
                return new FallingState();
            else
                return new RunningState();
        }
    }
}
