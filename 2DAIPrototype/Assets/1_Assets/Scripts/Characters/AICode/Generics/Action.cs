using Platformer.Scripts.Characters.Behaviours;
using UnityEngine;

namespace Platformer.Scripts.Characters.AI.Newcode
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Act(AiInputHandler controller);
    }
}
