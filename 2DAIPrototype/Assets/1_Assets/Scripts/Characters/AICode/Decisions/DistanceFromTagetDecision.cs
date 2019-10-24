using Platformer.Scripts.Characters.Behaviours;
using UnityEngine;

namespace Platformer.Scripts.Characters.AI.Newcode
{
    [CreateAssetMenu(menuName = "AI/Decisions/DistanceFromTagetDecision")]
    public class DistanceFromTagetDecision : Decision
    {
        public float distance;
        public Equality equality = Equality.greaterThan;

        public override bool Decide(AiInputHandler controller)
        {
            if (equality == Equality.greaterThan) return Vector2.Distance(controller.gameObject.transform.position, controller.target.gameObject.transform.position) > distance;
            
            return Vector2.Distance(controller.gameObject.transform.position, controller.target.gameObject.transform.position) < distance;
        }

        public enum Equality
        {
            greaterThan, lessThan
        };
    }
}
