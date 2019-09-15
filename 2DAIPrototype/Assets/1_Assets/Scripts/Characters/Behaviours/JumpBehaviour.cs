using System.Linq;
using UnityEngine;

namespace Platformer.Scripts.Characters.Behaviours
{
    [RequireComponent(typeof(ColliderBehaviour))]
    public class JumpBehaviour : MonoBehaviour
    {

        [SerializeField] private float maxJumpHeight = 4;
        [SerializeField] private float minJumpHeight = 1;

        private ColliderBehaviour coll;
        private IController ctrl;

        private float minJumpVelocity;

        private void Start()
        {
            ctrl = gameObject.GetComponents<Component>().OfType<IController>().FirstOrDefault();

            if (ctrl == null)
            {
                Debug.LogError("Interface not found!");
                return;
            }

            ctrl.OnJump += HandleJumpPressed;
            ctrl.OnJumpReleased += HandleJumpReleased;

            coll = gameObject.GetComponent<ColliderBehaviour>();

            minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(coll.gravity) * minJumpHeight);
        }

        private void OnDestroy()
        {
            if (ctrl == null)
            {
                return;
            }

            ctrl.OnJump -= HandleJumpPressed;
            ctrl.OnJumpReleased -= HandleJumpReleased;
        }

        private void HandleJumpPressed()
        {
            if (coll.collisions.below)
            {
                coll.AddImpulseForce(new Vector2(0, maxJumpHeight / coll.gravity));
            }
        }

        private void HandleJumpReleased()
        {

        }
    }
}
