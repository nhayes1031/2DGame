using Platformer.Scripts.Characters.Behaviours;
using UnityEngine;

namespace Platformer.Scripts.Characters.AI
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Act(AIControllerBehaviour controller);
    }
}
