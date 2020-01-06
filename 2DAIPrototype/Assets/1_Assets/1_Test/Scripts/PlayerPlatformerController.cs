using System.Collections;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{
    //TODO: Dash isn't working correctly. It won't successfully dash on left shift

    public float maxSpeed = 7f;
    public float jumpTakeOffSpeed = 7f;

    private int allowedJumps = 1;
    private int currentJumps = 0;

    private int allowedDashes = 0;
    private int currentDash = 0;
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashCooldown = 1f;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        groundedChanged += onGroundedChanged;
    }

    protected void onGroundedChanged()
    {
        if (Grounded == true)
            currentJumps = 0;
    }

    public void EnableDoubleJump()
    {
        allowedJumps = 2;
    }

    public void EnableDash()
    {
        allowedDashes = 1;
    }

    public void ResetDynamicAbility()
    {
        allowedJumps = 1;
        allowedDashes = 0;
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
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && currentJumps < allowedJumps)
        {
            currentJumps++;
            animator.SetTrigger("Jump");
            velocity.y = jumpTakeOffSpeed;
        } 
        else if (Input.GetButtonUp("Jump"))
        {
            if (move.y > 0)
                velocity.y = velocity.y * .5f;
        }

        if (currentDash < allowedDashes && Input.GetButtonDown("Fire3"))
        {
            StartCoroutine(Boost());
        }

        targetVelocity = move * maxSpeed;

        animator.SetBool("IsGrounded", Grounded);
        animator.SetFloat("Magnitude", targetVelocity.magnitude);

        if (velocity.y < 0)
            animator.SetBool("IsFalling", true);
        else
            animator.SetBool("IsFalling", false);

        if (Grounded)
        {
            currentDash = 0;
        }
    }

    IEnumerator Boost()
    {
        currentDash++;
        float dashTimer = .5f;
        float elapsedTime = 0;
        while (elapsedTime < dashTimer)
        {
            elapsedTime += Time.deltaTime;
            rb2D.velocity *= dashSpeed;
            yield return 0;
        }
        yield return new WaitForSeconds(dashCooldown);
    }
}
