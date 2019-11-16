using UnityEngine;

public class StopMovingOnGroundContact : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
            rb2D.velocity = Vector2.zero;
            rb2D.angularVelocity = 0;
        }
    }
}
