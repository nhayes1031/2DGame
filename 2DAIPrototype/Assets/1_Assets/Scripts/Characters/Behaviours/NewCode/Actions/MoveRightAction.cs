using Platformer.Scripts.Characters.Behaviours;
using UnityEngine;

namespace Platformer.Scripts.Characters.StateMachine
{
    [CreateAssetMenu(menuName = "Character/StateMachine/Actions/MoveRight")]
    public class MoveRightAction : Action
    {
        [SerializeField] private int moveSpeed;

        private float accelerationTimeGrounded = 0.01f;
        private float velocityXSmoothing;

        public override void Act(GameObject obj) {
            ColliderBehaviour coll = obj.GetComponentInChildren<ColliderBehaviour>();
            Animator anim = obj.GetComponent<Animator>();

            Vector2 velocity = CalculateVelocity(Vector2.right, coll.Velocity);

            anim.SetFloat(Animator.StringToHash("X"), velocity.x);
            anim.SetFloat(Animator.StringToHash("Y"), velocity.y);

            coll.Velocity = velocity;
        }

        private Vector2 CalculateVelocity(Vector2 desiredMove, Vector2 currentVelocity)
        {
            float targetVelocityX = desiredMove.x * moveSpeed;
            Vector2 newVelocity = currentVelocity;
            newVelocity.x = Mathf.SmoothDamp(newVelocity.x, targetVelocityX, ref velocityXSmoothing, accelerationTimeGrounded);
            return newVelocity;
        }

        public override void OnEnter() {
            return;
        }

        public override void OnExit() {
            return;
        }
    }
}
