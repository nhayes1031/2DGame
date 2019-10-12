using Platformer.Scripts.Characters.Behaviours;
using UnityEngine;

namespace Platformer.Scripts.Characters.AI.Newcode
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(AiInputHandler controller);
    }
}
