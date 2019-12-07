using UnityEngine;

public class SpawnWithVelocity : MonoBehaviour
{
    [SerializeField] private float gravityScale = 1;

    public void Start()
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();

        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float gravity = Physics.gravity.magnitude;
        float angle = Vector2.Angle(transform.position, target) * Mathf.Deg2Rad;

        float distance = Vector2.Distance(transform.position, target);
        float yOffset = transform.position.y - target.y;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        float angleBetweenObjects = Vector3.Angle(Vector3.forward, target - (Vector2)transform.position) * (target.x > transform.position.x ? 1 : -1);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector2.up) * velocity;

        rb2d.AddForce(finalVelocity * rb2d.mass, ForceMode2D.Impulse);

        // target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // target_Distance = Vector2.Distance(transform.position, target);
        // StartCoroutine(SimulateProjectile());
    }

    //IEnumerator SimulateProjectile() { 
    //    float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

    //    float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
    //    float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

    //    float elapse_time = 0;
    //    while (true)
    //    {
    //        transform.Translate(Vx * Time.deltaTime, (Vy - (gravity * elapse_time)) * Time.deltaTime, 0);
    //        elapse_time += Time.deltaTime;

    //        yield return null;
    //    }
    //}
}
