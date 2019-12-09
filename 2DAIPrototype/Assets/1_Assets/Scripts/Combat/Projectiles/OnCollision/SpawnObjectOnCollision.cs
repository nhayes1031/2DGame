using UnityEngine;

public class SpawnObjectOnCollision : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    private BoxCollider2D objectToSpawnCollider;

    private void Awake()
    {
        objectToSpawnCollider = objectToSpawn.GetComponentInChildren<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 8 && collision.gameObject.layer != 12)
        {
            Vector3 size = objectToSpawnCollider.bounds.size;
            Vector3 center = transform.position;

            for (int i = 0; i < 5; i++)
            {
                Collider2D[] colliders = Physics2D.OverlapBoxAll(center, size, 0, 9);
                if (colliders.Length == 0)
                {
                    Instantiate(objectToSpawn, center, Quaternion.Euler(0, 0, 0));
                    return;
                }

                center += new Vector3(0, objectToSpawnCollider.bounds.extents.y, 0);
            }
        }
    }
}
