using UnityEngine;

namespace Platformer.Scripts.Combat
{
    [CreateAssetMenu(menuName = "Combat/Projectile/Spawn/AtOrigin")]
    public class SpawnAtOrigin : ProjectileSpawner
    {
        public override void Run(GameObject obj)
        {
            Vector2 psp = findProjectileSpawnPoint(obj);
            Instantiate(objectToSpawn, psp, Quaternion.Euler(0, 0, 0));
        }
    }
}
