using UnityEngine;

namespace Platformer.Scripts.Combat
{
    public abstract class ProjectileSpawner : ScriptableObject
    {
        public GameObject objectToSpawn;
        public abstract void Run(Vector2 originPoint);
    }
}
