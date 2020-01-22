using System.Collections;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{
    public float maxSpeed = 7f;
    public float jumpTakeOffSpeed = 7f;

    private int allowedJumps = 1;
    private int currentJumps = 0;

    private int allowedDashes = 0;
    private int currentDash = 0;
    private bool isDashing = false;
    [SerializeField] private float dashSpeed = 7f;
    [SerializeField] private float dashCooldown = 1f;

    private Vector2 input = Vector2.zero;

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
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        if (Input.GetButtonDown("Jump"))
            Jump();
        else if (Input.GetButtonDown("Fire3"))
            Dash();
        else
            Move();

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
        if (Input.GetButtonDown("Jump") && currentJumps < allowedJumps)
        {
            currentJumps++;
            animator.SetTrigger("Jump");
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (targetVelocity.y > 0)
                velocity.y = velocity.y * .5f;
        }
    }

    private void Dash()
    {
        if (currentDash < allowedDashes && Input.GetButtonDown("Fire3"))
        {
            currentDash++;
            isDashing = true;
            StartCoroutine(Boost(input));
        }

        if (Grounded)
        {
            currentDash = 0;
        }
    }

    private void Move()
    {
        Vector2 move = Vector2.zero;

        if (!isDashing)
            move.x = input.x;

        targetVelocity = move * maxSpeed;
    }

    IEnumerator Boost(Vector2 move)
    {
        float dashTimer = .5f;
        float elapsedTime = 0;
        while (elapsedTime < dashTimer)
        {
            elapsedTime += Time.deltaTime;
            targetVelocity *= dashSpeed;
            yield return 0;
        }
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
    }
}
