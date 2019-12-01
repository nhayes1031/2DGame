using Platformer.Scripts.CameraManagement;
using UnityEngine;

public class MindControlOnCollision : MonoBehaviour
{ 
    public void OnTriggerEnter2D(Collider2D collision)
    {
        int parentId = GetComponent<ProjectileData>().ParentId;
        if (collision.gameObject.layer == 12 && parentId != collision.gameObject.GetInstanceID())
        {
            Possessable possessable = collision.gameObject.GetComponentInParent<Possessable>();
            possessable.Possessed();

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Destroy(player);

            GameObject camera = GameObject.Find("CameraManager");
            CameraManager cm = camera.GetComponent<CameraManager>();
            cm.Follow(collision.gameObject.transform);
        }
    }
}
