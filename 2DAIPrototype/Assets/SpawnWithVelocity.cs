using System.Collections;
using UnityEngine;

public class SpawnWithVelocity : MonoBehaviour
{
    public float firingAngle = 45.0f;

    private Vector2 target;
    [SerializeField]
    private float gravity = -9.81f;

    private float target_Distance;

    public void Start()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.y = transform.position.y;
        Debug.Log(target);
        target_Distance = Vector2.Distance(transform.position, target);
        StartCoroutine(SimulateProjectile());
    }

    IEnumerator SimulateProjectile() { 
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        float flightDuration = target_Distance / Vx;

        float elapse_time = 0;
        while (elapse_time < flightDuration)
        {
            transform.Translate(Vx * Time.deltaTime, (Vy - (gravity * elapse_time)) * Time.deltaTime, 0);
            elapse_time += Time.deltaTime;

            yield return null;
        }
    }
}
