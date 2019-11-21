using UnityEngine;

namespace Platformer.Scripts.Combat
{
    [CreateAssetMenu(menuName = "Combat/Projectile/Spawn/TowardsCursor")]
    public class SpawnTowardsCursor : ProjectileSpawner
    {
        public GameObject objectToSpawn;

        public override void Run(Vector2 originPoint)
        {
            Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float rotationZ = Mathf.Atan2(Mathf.Abs(mouseScreenPosition.y - originPoint.y), Mathf.Abs(mouseScreenPosition.x - originPoint.x));
            float dotProd = CrossProduct(mouseScreenPosition, originPoint);
            Debug.Log(dotProd);
            float rotationY = dotProd < 0 ? 0 : 180f;
            Instantiate(objectToSpawn, originPoint, Quaternion.Euler(0, rotationY, rotationZ));
        }

        private float CrossProduct(Vector2 A, Vector2 B)
        {
            return -A.x * B.y + A.y * B.x;
        }
    }
}
