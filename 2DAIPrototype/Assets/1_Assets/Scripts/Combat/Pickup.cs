using System.Collections;
using UnityEngine;

namespace Platformer.Scripts.Combat
{
    public class Pickup : MonoBehaviour
    {

        public float TimeBeforeBeingPickupable = 1.0f;
        public int healAmount = 10;

        private bool pickupable = false;

        private void Start()
        {
            if (TimeBeforeBeingPickupable > 0)
            {
                StartCoroutine(DelayPickup());
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.tag == "Player" && pickupable)
            {
                collision.GetComponentInParent<Health>().TakeDamage(-healAmount);
                Destroy(gameObject);
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.transform.tag == "Player" && pickupable)
            {
                collision.GetComponentInParent<Health>().TakeDamage(-healAmount);
                Destroy(gameObject);
            }
        }

        IEnumerator DelayPickup()
        {
            yield return new WaitForSeconds(TimeBeforeBeingPickupable);
            pickupable = true;
        }
    }
}
