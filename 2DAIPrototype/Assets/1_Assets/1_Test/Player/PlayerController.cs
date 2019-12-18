using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void Update()
    {
        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Jump();
        Move();
        FlipSprite(mouseScreenPosition);
        Aim(mouseScreenPosition);
        Attack();
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("Jump");
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        anim.SetFloat("Magnitude", Mathf.Abs(horizontal));
    }

    private void FlipSprite(Vector2 msp)
    {
        if (msp.x < transform.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        } else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void Aim(Vector2 msp)
    {
        float rotationZ = Mathf.Rad2Deg * Mathf.Atan2(Mathf.Abs(msp.y - transform.position.y), Mathf.Abs(msp.x - transform.position.x));

        float aimAngle = rotationZ * (msp.y >= transform.position.y ? 1 : -1);
        anim.SetFloat("AimAngle", aimAngle);
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Left Click"))
        {
            anim.SetTrigger("Special");
        }
    }

    private float CrossProduct(Vector2 A, Vector2 B)
    {
        return -A.x * B.y + A.y * B.x;
    }
}
