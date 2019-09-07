using Platformer.Scripts.Characters.Behaviours;
using UnityEngine;

namespace Platformer.Scripts.Characters.AI
{
    [CreateAssetMenu(menuName = "AI/Decisions/Grounded")]
    public class GroundedDecision : Decision
    {

        public override bool Decide(AIControllerBehaviour controller)
        {
            return controller.coll.collisions.below;
        }
    }
}
