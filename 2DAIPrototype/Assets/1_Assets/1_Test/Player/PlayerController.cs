using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("Jump");
        }

        float horizontal = Input.GetAxis("Horizontal");
        anim.SetFloat("Magnitude", Mathf.Abs(horizontal));

        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float rotationZ = Mathf.Rad2Deg * Mathf.Atan2(Mathf.Abs(mouseScreenPosition.y - transform.position.y), Mathf.Abs(mouseScreenPosition.x - transform.position.x));
        float aimAngle = rotationZ * (mouseScreenPosition.y >= transform.position.y ? 1 : -1);
        anim.SetFloat("AimAngle", aimAngle);

        // This breaks in quadrant 3
        bool isLeft = CrossProduct(mouseScreenPosition, transform.position) > 1 ? false : true;
        if (isLeft)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        } else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

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
