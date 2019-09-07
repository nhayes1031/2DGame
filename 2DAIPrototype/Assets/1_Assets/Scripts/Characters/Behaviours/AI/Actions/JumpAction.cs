using Platformer.Scripts.Characters.Behaviours;
using UnityEngine;

namespace Platformer.Scripts.Characters.AI
{
    [CreateAssetMenu(menuName = "AI/Actions/Jump")]
    public class JumpAction : Action
    {
        [SerializeField]
        [Range(0, 2)]
        float jumpFrequency = 0.5f;
        [SerializeField]
        [Range(1, 100)]
        int jumpChance = 45;

        public override void Act(AIControllerBehaviour controller)
        {
            Jump(controller);
        }

        private void Jump(AIControllerBehaviour controller)
        {
            if (controller.coll.collisions.below)
            {
                if (controller.jumpTimer < Time.realtimeSinceStartup)
                {
                    controller.jumpTimer = Time.realtimeSinceStartup + jumpFrequency;
                    int num = Random.Range(0, 100);
                    if (num <= jumpChance)
                    {
                        controller.TriggerOnJump();
                    }
                }
            }
        }
    }
}
