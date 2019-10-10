using UnityEngine;

namespace Platformer.Scripts.Characters.StateMachine
{
    [CreateAssetMenu(menuName = "Character/StateMachine/Actions/Attack")]
    public class AttackAction : Action
    {
        public override void Act(GameObject obj) {
            Debug.Log("Attacking");
        }

        public override void OnEnter() {
            return;
        }

        public override void OnExit() {
            return;
        }
    }
}
