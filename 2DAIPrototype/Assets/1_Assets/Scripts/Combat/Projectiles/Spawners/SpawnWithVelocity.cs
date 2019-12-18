using UnityEngine;

public class SpawnWithVelocity : MonoBehaviour
{
    [SerializeField] private float heightAtApex = 5;

    public void Start()
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float gravity = -Physics.gravity.magnitude;
        float distanceAlongX = target.x - transform.position.x;
        float distanceAlongY = target.y - transform.position.y;
        float height = Mathf.Max(heightAtApex + distanceAlongY, 0);

        float xVelocity = distanceAlongX / (Mathf.Sqrt((-2 * height) / gravity) + Mathf.Sqrt((2 * (distanceAlongY - height)) / gravity));
        float yVelocity = Mathf.Sqrt(-2 * gravity * height);

        Vector2 initialVelocity = new Vector2(xVelocity, yVelocity);
        rb2d.AddForce(initialVelocity, ForceMode2D.Impulse);
    }
}
