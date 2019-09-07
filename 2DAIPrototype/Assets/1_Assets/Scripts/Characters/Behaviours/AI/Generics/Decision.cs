using Platformer.Scripts.Characters.Behaviours;
using UnityEngine;

namespace Platformer.Scripts.Characters.AI
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(AIControllerBehaviour controller);
    }
}
