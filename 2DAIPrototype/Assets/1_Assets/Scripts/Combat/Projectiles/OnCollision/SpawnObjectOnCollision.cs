using UnityEngine;

public class SpawnObjectOnCollision : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Instantiate(objectToSpawn, transform.position, transform.rotation);
        }
    }

    private void CheckIfValidSpawnPosition()
    {
        // TODO: Loop using box cast and find if there is a suitable spawn location.
    }
}
