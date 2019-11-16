using Platformer.Scripts.Combat;
using UnityEngine;

namespace Platformer.Scripts.Characters.StateMachine
{
    [CreateAssetMenu(menuName = "Character/StateMachine/Actions/ThrowProjectile")]
    public class ThrowProjectile : Action
    {
        public GameObject objectToSpawn;
        public float force = 1.0f;
        [Range(0, 360)]
        public int direction = 0;
        public int healthCost = 0;

        public override void Act(GameObject obj)
        {
            Health health = obj.GetComponent<Health>();
            health.TakeDamage(healthCost);

            GameObject projectile = Instantiate(objectToSpawn, obj.transform.position, obj.transform.rotation);
            Rigidbody2D rb2D = projectile.GetComponent<Rigidbody2D>();
            Vector2 initialVelocity = Vector2.zero;
            initialVelocity.x = Mathf.Cos(Mathf.Deg2Rad * direction) + force;
            initialVelocity.y = Mathf.Sin(Mathf.Deg2Rad * direction) + force;
            rb2D.AddForce(initialVelocity, ForceMode2D.Impulse);
        }

        public override void OnEnter()
        {
            return;
        }

        public override void OnExit()
        {
            return;
        }
    }
}
