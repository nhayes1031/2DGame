using UnityEngine;

public class PlayerAimController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        FlipSprite(mouseScreenPosition);
        Aim(mouseScreenPosition);
        Attack();
    }

    private void Aim(Vector2 msp)
    {
        float rotationZ = Mathf.Rad2Deg * Mathf.Atan2(Mathf.Abs(msp.y - transform.position.y), Mathf.Abs(msp.x - transform.position.x));

        float aimAngle = rotationZ * (msp.y >= transform.position.y ? 1 : -1);
        animator.SetFloat("AimAngle", aimAngle);
    }

    private void FlipSprite(Vector2 msp)
    {
        if (msp.x < transform.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Left Click"))
        {
            animator.SetTrigger("Special");
        }
    }
}
