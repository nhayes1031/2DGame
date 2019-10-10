using UnityEngine;

namespace Platformer.Scripts.Characters.StateMachine
{
    [CreateAssetMenu(menuName = "Character/StateMachine/Actions/Move")]
    public class MoveAction : Action
    {
        public override void Act(GameObject obj) {
            Debug.Log("Walking");
        }

        public override void OnEnter() {
            return;
        }

        public override void OnExit() {
            return;
        }
    }
}
