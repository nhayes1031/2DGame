﻿using Platformer.Scripts.Combat;
using UnityEngine;

public class ExplodeOnCollision : MonoBehaviour
{
    public int damage = 10;
    public float radius = 0.5f;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 8 && collision.gameObject.layer == 9 || collision.gameObject.layer == 10)
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
            Destroy(gameObject);
        }

        Debug.DrawLine(transform.position, (Vector2)transform.position + (Vector2.up * radius));
        Debug.DrawLine(transform.position, (Vector2)transform.position + (Vector2.right * radius));
        Debug.DrawLine(transform.position, (Vector2)transform.position + (Vector2.down * radius));
        Debug.DrawLine(transform.position, (Vector2)transform.position + (Vector2.left * radius));
    }
}
