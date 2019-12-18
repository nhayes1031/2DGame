using Platformer.Scripts.Combat;
using UnityEngine;

public class ExplodeOnCollision : MonoBehaviour
{
    public int damage = 10;
    public float radius = 0.5f;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        int parentId = GetComponent<ProjectileData>().ParentId;
        if (parentId != collider.transform.parent.GetInstanceID())
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(gameObject.transform.position, radius);
            foreach (Collider2D hit in hits)
            {
                Health health = hit.GetComponentInParent<Health>();
                if (health)
                {
                    health.TakeDamage(damage);
                }
            }

            Debug.DrawLine(transform.position, (Vector2)transform.position + (Vector2.up * radius));
            Debug.DrawLine(transform.position, (Vector2)transform.position + (Vector2.right * radius));
            Debug.DrawLine(transform.position, (Vector2)transform.position + (Vector2.down * radius));
            Debug.DrawLine(transform.position, (Vector2)transform.position + (Vector2.left * radius));
        }
    }
}