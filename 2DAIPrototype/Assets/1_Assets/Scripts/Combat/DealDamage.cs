using Platformer.Scripts.Combat;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public int damage = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponentInParent<Health>().TakeDamage(damage);
    }
}
