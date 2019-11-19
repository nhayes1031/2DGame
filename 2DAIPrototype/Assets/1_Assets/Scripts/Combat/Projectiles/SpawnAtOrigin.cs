using System;
using UnityEngine;

namespace Platformer.Scripts.Combat
{
    [CreateAssetMenu(menuName = "Combat/Projectile/Spawn/AtOrigin")]
    public class SpawnAtOrigin : ProjectileSpawner
    {
        public GameObject objectToSpawn;
        public override void Run(Vector2 originPoint)
        {
            Instantiate(objectToSpawn, originPoint, Quaternion.Euler(0, 0, 0));
        }
    }
}
