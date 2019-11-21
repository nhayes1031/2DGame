using UnityEngine;

namespace Platformer.Scripts.Combat
{
    [CreateAssetMenu(menuName = "Combat/Projectile/Spawn/TowardsCursor")]
    public class SpawnTowardsCursor : ProjectileSpawner
    {
        public GameObject objectToSpawn;

        public override void Run(Vector2 originPoint)
        {
            Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 difference = mouseScreenPosition - originPoint;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            Instantiate(objectToSpawn, originPoint, Quaternion.Euler(0, 0, rotationZ));
        }
    }
}
