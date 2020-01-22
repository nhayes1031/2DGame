using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._1_Assets._1_Test.Scripts.State
{
    class DashingState : State
    {
        private bool dashCompleted = false;
        private bool isDashing = false;

        private float dashSpeed = 10f;
        private float dashDuration = 1f;

        public State Update(PhysicsObject po)
        {
            Debug.Log("Dashing State");

            if (!isDashing)
            {
                isDashing = true;

                Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                Vector2 normalizedDirection = direction.normalized;

                po.targetVelocity = normalizedDirection * dashSpeed * 10;

                po.StartCoroutine(CompleteDash());
            }

            if (dashCompleted)
            {
                if (Input.GetButtonDown("Jump"))
                    return new JumpingState();
                if (!po.grounded)
                    return new FallingState();
                else
                    return new RunningState();
            }
            return this;
        }

        private IEnumerator CompleteDash()
        {
            yield return new WaitForSeconds(dashDuration);

            dashCompleted = true;
        }
    }
}
