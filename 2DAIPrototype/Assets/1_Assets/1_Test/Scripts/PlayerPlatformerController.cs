using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{
    public float maxSpeed = 7f;
    public float jumpTakeOffSpeed = 7f;

    private int allowedJumps = 1;
    private int currentJumps = 0;

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

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && currentJumps < allowedJumps)
        {
            currentJumps++;
            animator.SetTrigger("Jump");
            velocity.y = jumpTakeOffSpeed;
        } else if (Input.GetButtonUp("Jump"))
        {
            if (move.y > 0)
                velocity.y = velocity.y * .5f;
        }

        targetVelocity = move * maxSpeed;

        animator.SetBool("IsGrounded", Grounded);
        animator.SetFloat("Magnitude", targetVelocity.magnitude);

        if (velocity.y < 0)
            animator.SetBool("IsFalling", true);
        else
            animator.SetBool("IsFalling", false);
    }
}
