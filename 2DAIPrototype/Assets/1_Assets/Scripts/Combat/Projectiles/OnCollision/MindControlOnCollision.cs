using Platformer.Scripts.CameraManagement;
using Platformer.Scripts.Characters.Behaviours;
using SA;
using UnityEngine;

public class MindControlOnCollision : MonoBehaviour
{ 
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12 && !collision.tag.Equals("Player"))
        {
            // This one gets triggered

            // The player and enemies shouldn't have different layers/tags.
            // The only difference should be where they get inputs

            // Add Hardware Input Handler
            // Turn off State Manager
            // Destroy Player

            // Don't check the tag for Controllable.
            // Just do a getComponent check and see if there is a IControllable script

            IControllable controllable = collision.gameObject.GetComponent<IControllable>();
            controllable.Possessed();
            // Destroy(Player);

            GameObject camera = GameObject.Find("CameraManager");
            CameraManager cm = camera.GetComponent<CameraManager>();
            cm.Follow(collision.gameObject.transform);
            Destroy(gameObject);
        }
    }
}
