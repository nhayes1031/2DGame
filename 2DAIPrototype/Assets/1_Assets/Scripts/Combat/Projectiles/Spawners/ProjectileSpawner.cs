using UnityEngine;

namespace Platformer.Scripts.Combat
{
    public abstract class ProjectileSpawner : ScriptableObject
    {
        public GameObject objectToSpawn;
        public abstract void Run(GameObject obj);

        public Vector2 findProjectileSpawnPoint(GameObject obj)
        {
            return obj.GetComponent<ProjectileSpawnPoint>().spawnPoint.position;
        }
    }
}
