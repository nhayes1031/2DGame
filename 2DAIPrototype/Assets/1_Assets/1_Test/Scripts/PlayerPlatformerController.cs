using Assets._1_Assets._1_Test.Scripts.State;
using System.Collections;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{
    public float jumpTakeOffSpeed = 7f;

    private int allowedJumps = 1;
    private int currentJumps = 0;

    private Vector2 input = Vector2.zero;

    private Animator animator;

    private State state = new RunningState();

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void EnableDoubleJump()
    {
        Debug.Log("Double Jump Enabled");
    }

    private void EnableDash()
    {
        Debug.Log("Dash Enabled");
    }

    public void ResetDynamicAbility()
    {
        Debug.Log("Dash and Double Jump Disabled");
    }

    public void SetDynamicAbility(AbilityNames abilityName)
    {
        ResetDynamicAbility();
        switch (abilityName) {
            case AbilityNames.DoubleJump:
                EnableDoubleJump();
                break;
            case AbilityNames.Dash:
                EnableDash();
                break;
        }
    }

    protected override void ComputeVelocity()
    {
        State newState = state.Update(this);
        if (newState != null)
        {
            state = newState;
        }

        AssignAnimatorVariables();
    }
    private void AssignAnimatorVariables()
    {
        animator.SetBool("IsGrounded", Grounded);
        animator.SetFloat("Magnitude", targetVelocity.magnitude);

        if (velocity.y < 0)
            animator.SetBool("IsFalling", true);
        else
            animator.SetBool("IsFalling", false);
    }

    private void Jump()
    {
        if (Input.GetButtonUp("Jump"))
            if (targetVelocity.y > 0)
                velocity.y = velocity.y * .5f;
        else
        {
            animator.SetTrigger("Jump");
            velocity.y = jumpTakeOffSpeed;
        }
    }
}
