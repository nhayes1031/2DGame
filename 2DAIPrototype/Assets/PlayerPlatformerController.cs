using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{
    // TODO:
    // 1. Don't flip sprite based off of movement. Flip it off of cursor position

    public float maxSpeed = 7f;
    public float jumpTakeOffSpeed = 7f;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            animator.SetTrigger("Jump");
            velocity.y = jumpTakeOffSpeed;
        } else if (Input.GetButtonUp("Jump"))
        {
            if (move.y > 0)
                velocity.y = velocity.y * .5f;
        }

        targetVelocity = move * maxSpeed;

        animator.SetBool("IsGrounded", grounded);
        animator.SetFloat("Magnitude", targetVelocity.magnitude);

        if (velocity.y < 0)
            animator.SetBool("IsFalling", true);
        else
            animator.SetBool("IsFalling", false);
    }
}
