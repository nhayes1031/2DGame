using Platformer.Scripts.Characters.Behaviours;
using UnityEngine;

namespace Platformer.Scripts.Characters.StateMachine
{
    [CreateAssetMenu(menuName = "Character/StateMachine/Actions/Jump")]
    public class JumpAction : Action
    {
        [SerializeField] private float maxJumpHeight = 4;

        public override void Act(GameObject obj) {
            ColliderBehaviour coll = obj.GetComponentInChildren<ColliderBehaviour>();
            if (coll.collisions.below)
            {
                coll.AddImpulseForce(new Vector2(0, maxJumpHeight / coll.gravity));
            }
        }

        public override void OnEnter() {
            return;
        }

        public override void OnExit() {
            return;
        }
    }
}
