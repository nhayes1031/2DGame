using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._1_Assets._1_Test.Scripts.State
{
    class RunningState : State
    {
        private float maxSpeed = 7f;

        public State Update(PhysicsObject po)
        {
            Vector2 move = Vector2.zero;
            move.x = Input.GetAxis("Horizontal");
            po.targetVelocity = move * maxSpeed;

            Animator animator = po.GetComponent<Animator>();
            if (animator != null)
                animator.SetFloat("Magnitude", po.targetVelocity.magnitude);

            if (Input.GetButtonDown("Jump"))
                return new JumpingState();
            if (Input.GetButtonDown("Fire3"))
                return new DashingState();
            if (!po.grounded)
                return new FallingState();
            return this;
        }
    }
}
