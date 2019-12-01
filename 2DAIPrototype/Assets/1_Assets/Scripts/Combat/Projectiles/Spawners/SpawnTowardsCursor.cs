using UnityEngine;

namespace Platformer.Scripts.Combat
{
    [CreateAssetMenu(menuName = "Combat/Projectile/Spawn/TowardsCursor")]
    public class SpawnTowardsCursor : ProjectileSpawner
    {
        public override void Run(GameObject obj)
        {
            Vector2 psp = findProjectileSpawnPoint(obj);
            Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float rotationZ = Mathf.Atan2(Mathf.Abs(mouseScreenPosition.y - psp.y), Mathf.Abs(mouseScreenPosition.x - psp.x));
            float dotProd = CrossProduct(mouseScreenPosition, psp);
            float rotationY = dotProd < 0 ? 0 : 180f;
            ProjectileData pd = Instantiate(objectToSpawn, psp, Quaternion.Euler(0, rotationY, rotationZ)).GetComponent<ProjectileData>();
            pd.SetParentId(obj.gameObject.GetInstanceID());
        }

        private float CrossProduct(Vector2 A, Vector2 B)
        {
            return -A.x * B.y + A.y * B.x;
        }
    }
}
