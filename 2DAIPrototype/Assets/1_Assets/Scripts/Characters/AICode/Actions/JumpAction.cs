using Platformer.Scripts.Characters.Behaviours;
using UnityEngine;

namespace Platformer.Scripts.Characters.AI.Newcode
{
    [CreateAssetMenu(menuName = "AI/Actions/Jump2")]
    public class JumpAction : Action
    {
        [SerializeField]
        [Range(0, 2)]
        float jumpFrequency = 0.5f;
        [SerializeField]
        [Range(1, 100)]
        int jumpChance = 45;

        JumpCommand jumpCom = new JumpCommand();

        public override void Act(AiInputHandler controller)
        {
            Jump(controller);
        }

        private void Jump(AiInputHandler controller)
        {
            if (controller.coll.collisions.below)
            {
                if (controller.jumpTimer < Time.realtimeSinceStartup)
                {
                    controller.jumpTimer = Time.realtimeSinceStartup + jumpFrequency;
                    int num = Random.Range(0, 100);
                    if (num <= jumpChance)
                    {
                        jumpCom.Execute(controller.gameObject);
                    }
                }
            }
        }
    }
}
