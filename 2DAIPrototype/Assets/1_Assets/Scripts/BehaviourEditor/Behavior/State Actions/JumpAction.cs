using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "AI2/Actions/Jump")]
    public class JumpAction : StateActions
    {
        [SerializeField]
        [Range(0, 2)]
        float jumpFrequency = 0.5f;
        [SerializeField]
        [Range(1, 100)]
        int jumpChance = 45;

        JumpCommand jumpCom = new JumpCommand();

        public override void Execute(StateManager states)
        {
            Jump(states);
        }

        private void Jump(StateManager states)
        {
            if (states.coll.collisions.below)
            {
                if (states.jumpTimer < Time.realtimeSinceStartup)
                {
                    states.jumpTimer = Time.realtimeSinceStartup + jumpFrequency;
                    int num = Random.Range(0, 100);
                    if (num <= jumpChance)
                    {
                        jumpCom.Execute(states.gameObject);
                    }
                }
            }
        }
    }
}
