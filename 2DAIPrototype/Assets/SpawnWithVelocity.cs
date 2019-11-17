using UnityEngine;

public class SpawnWithVelocity : MonoBehaviour
{
    public float force = 5f;

    public void Start()
    {
        Rigidbody2D rb2D = transform.GetComponent<Rigidbody2D>();
        if (rb2D != null) rb2D.AddForce(Vector2.right * force, ForceMode2D.Impulse);
    }
}
