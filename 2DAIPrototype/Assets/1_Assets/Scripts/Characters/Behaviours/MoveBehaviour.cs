using Platformer.Saving;
using System.Linq;
using UnityEngine;

namespace Platformer.Scripts.Characters.Behaviours
{
    [RequireComponent(typeof(ColliderBehaviour))]
    public class MoveBehaviour : MonoBehaviour, ISaveable
    {
        [SerializeField] private int moveSpeed;

        private ColliderBehaviour coll;
        private IController ctrl;

        private float accelerationTimeGrounded = 0.01f;
        private float velocityXSmoothing;
        private Vector2 velocity;

        private void Start()
        {
            ctrl = gameObject.GetComponents<Component>().OfType<IController>().FirstOrDefault();

            if (ctrl == null)
            {
                Debug.LogError("Interface not found!");
                return;
            }

            ctrl.OnMove += HandleOnMove;

            coll = gameObject.GetComponent<ColliderBehaviour>();
        }

        private void OnDestroy()
        {
            if (ctrl == null)
            {
                return;
            }

            ctrl.OnMove -= HandleOnMove;
        }

        private void HandleOnMove(Vector2 desiredMove)
        {
            Vector2 velocity = CalculateVelocity(desiredMove);
            coll.Velocity = velocity;
        }

        private Vector2 CalculateVelocity(Vector2 desiredMove)
        {
            float targetVelocityX = desiredMove.x * moveSpeed;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, accelerationTimeGrounded);
            return velocity;
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            transform.position = ((SerializableVector3)state).ToVector();
        }
    }
}
