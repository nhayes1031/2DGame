using Platformer.Scripts.Characters.Behaviours;
using UnityEngine;

namespace Platformer.Scripts.Characters.AI.Newcode
{
    [CreateAssetMenu(menuName = "AI/Actions/Attack")]
    public class AttackAction : Action
    {
        private AttackCommand attackCom = new AttackCommand();

        public override void Act(AiInputHandler controller)
        {
            attackCom.Execute(controller.gameObject);
        }
    }
}
