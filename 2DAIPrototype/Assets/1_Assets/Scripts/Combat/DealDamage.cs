using Platformer.Scripts.Combat;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public int damage = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponentInParent<Health2>().TakeDamage(damage);
    }
}
