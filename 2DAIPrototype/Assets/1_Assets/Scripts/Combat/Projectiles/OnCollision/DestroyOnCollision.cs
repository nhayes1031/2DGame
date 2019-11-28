using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    public GameObject origin;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (origin.GetInstanceID() != collision.gameObject.GetInstanceID())
        {
            Destroy(gameObject);
        }
    }
}
