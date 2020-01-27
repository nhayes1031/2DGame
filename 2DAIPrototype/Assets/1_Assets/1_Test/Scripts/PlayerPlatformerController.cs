using Assets._1_Assets._1_Test.Scripts.State;
using Assets._1_Assets._1_Test.Scripts;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{
    private Animator animator;
    private State state = new RunningState();
    private PlayerData playerData = new PlayerData();

    private void Awake()
    {
        animator = GetComponent<Animator>();
        groundedChanged += onGroundedChanged;
    }

    protected void onGroundedChanged()
    {
        if (Grounded == true)
        {
            playerData.ResetCurrentJumps();
            playerData.ResetCurrentDashes();
        }
    }

    private void EnableDoubleJump()
    {
        playerData.IncrementAllowedJumps();
    }

    private void EnableDash()
    {
        playerData.IncrementAllowedDashes();
    }

    private void ResetDynamicAbility()
    {
        playerData.ResetAllowedJumps();
        playerData.ResetAllowedDashes();
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
        state = state.Update(this, playerData);

        AssignAnimatorVariables();
    }

    private void AssignAnimatorVariables()
    {
        animator.SetBool("IsGrounded", Grounded);
        animator.SetFloat("Magnitude", TargetVelocity.magnitude);

        if (velocity.y < 0)
            animator.SetBool("IsFalling", true);
        else
            animator.SetBool("IsFalling", false);
    }
}
