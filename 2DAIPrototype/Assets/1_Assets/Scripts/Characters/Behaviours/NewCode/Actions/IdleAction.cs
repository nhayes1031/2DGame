using UnityEngine;

namespace Platformer.Scripts.Characters.StateMachine
{
    [CreateAssetMenu(menuName = "Character/StateMachine/Actions/Idle")]
    public class IdleAction : Action
    {
        public override void Act(GameObject obj) {
            Debug.Log("Idle");
        }

        public override void OnEnter() {
            return;
        }

        public override void OnExit() {
            return;
        }
    }
}
