using UnityEngine;

namespace Platformer.Core
{
    class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField]
        GameObject persistentObjectPrefab;

        static bool hasSpawned;

        private void Awake()
        {
            if (hasSpawned) return;

            hasSpawned = true;
            SpawnPersistentObjects();
        }

        private void SpawnPersistentObjects()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}
