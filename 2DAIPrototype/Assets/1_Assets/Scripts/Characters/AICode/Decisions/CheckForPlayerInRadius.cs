using Platformer.Scripts.Characters.Behaviours;
using UnityEngine;

namespace Platformer.Scripts.Characters.AI.Newcode
{
    [CreateAssetMenu(menuName = "AI/Decisions/CheckForPlayerInRadius")]
    public class CheckForPlayerInRadius : Decision
    {
        public float radius;

        public override bool Decide(AiInputHandler controller)
        {
            Collider2D[] collisions = Physics2D.OverlapCircleAll(controller.transform.position, radius, LayerMask.GetMask("Pushbox"));
            
            foreach(Collider2D collision in collisions)
            {
                if (collision.tag == "Player")
                {
                    controller.SetNewTarget(collision.gameObject);
                    return true;
                }
            }

            return false;
        }
    }
}
