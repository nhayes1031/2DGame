using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        int parentId = GetComponent<ProjectileData>().ParentId;
        if (parentId != collision.transform.parent.GetInstanceID())
        {
            Destroy(gameObject);
        }
    }
}
