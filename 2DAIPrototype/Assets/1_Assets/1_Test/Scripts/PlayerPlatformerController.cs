using Assets._1_Assets._1_Test.Scripts.State;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{
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
        state = state.Update(this);

        AssignAnimatorVariables();
    }

    private void AssignAnimatorVariables()
    {
        animator.SetBool("IsGrounded", grounded);
        animator.SetFloat("Magnitude", targetVelocity.magnitude);

        if (velocity.y < 0)
            animator.SetBool("IsFalling", true);
        else
            animator.SetBool("IsFalling", false);
    }
}
