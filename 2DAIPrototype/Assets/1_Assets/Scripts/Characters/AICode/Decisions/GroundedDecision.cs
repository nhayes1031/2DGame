using Platformer.Scripts.Characters.Behaviours;
using UnityEngine;

namespace Platformer.Scripts.Characters.AI.Newcode
{
    [CreateAssetMenu(menuName = "AI/Decisions/Grounded2")]
    public class GroundedDecision : Decision
    {
        public override bool Decide(AiInputHandler controller)
        {
            return controller.coll.collisions.below;
        }
    }
}
