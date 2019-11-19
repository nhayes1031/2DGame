using Platformer.Scripts.Combat;
using UnityEngine;

public class TransitionToCosumableOnCollision : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Destroy(GetComponent<SpawnWithVelocity>());
            Destroy(GetComponent<MindControlOnCollision>());
            Destroy(GetComponent<Rigidbody2D>());

            gameObject.AddComponent<Pickup>();
            // TODO: Pickup needs a pushbox to work correct.
            // Add a pushbox to a child component of this object.

            Destroy(GetComponent<TransitionToCosumableOnCollision>());
        }
    }
}
