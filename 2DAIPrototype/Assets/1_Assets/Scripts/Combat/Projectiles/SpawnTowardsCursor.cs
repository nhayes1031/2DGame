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
            float angleRad = Mathf.Atan2(mouseScreenPosition.y - originPoint.y, mouseScreenPosition.x - originPoint.x);
            float angleDeg = Mathf.Rad2Deg * angleRad;
            Instantiate(objectToSpawn, originPoint, Quaternion.Euler(0, 0, angleDeg));
        }
    }
}
