using UnityEngine;

namespace Platformer.Scripts.Combat
{
    public abstract class AbilityCooldown : ScriptableObject
    {
        public abstract void Run(GameObject obj);
    }
}
