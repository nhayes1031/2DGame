using UnityEngine;

namespace Platformer.Scripts.Combat
{
    public abstract class AbilityCost : ScriptableObject
    {
        public abstract void Run(GameObject obj);
    }
}
