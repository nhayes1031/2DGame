using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        ProjectileData pd = GetComponent<ProjectileData>();
        if (pd.ParentId != collision.gameObject.GetInstanceID())
        {
            Destroy(gameObject);
        }
    }
}
