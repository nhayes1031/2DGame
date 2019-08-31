using UnityEngine;

public class LevelControl : MonoBehaviour {
    public GameObject spawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.gameObject.transform.position = spawnPoint.transform.position;
        }
    }
}
