using UnityEngine;

namespace Platformer.Scripts.Combat
{
    public class Hitbox : MonoBehaviour
    {
        public LayerMask mask;
        public bool useSphere = false;
        public Vector3 hitboxSize = Vector3.one;
        public float radius = 0.5f;
        public Color inactiveColor;
        public Color collisionOpenColor;
        public Color collidingColor;

        private ColliderState _state;

        private void Check()
        {
            if (_state == ColliderState.Closed) return;
            Collider[] colliders = UnityEngine.Physics.OverlapBox(transform.position, hitboxSize, transform.rotation, mask);

            if (colliders.Length > 0)
            {
                _state = ColliderState.Colliding;
                // We should do something with the colliders
            }
            else
            {
                _state = ColliderState.Open;
            }
        }

        public void startCheckingCollisions()
        {
            _state = ColliderState.Open;
        }

        public void stopCheckingCollisions()
        {
            _state = ColliderState.Closed;
        }

        private void OnDrawGizmos()
        {
            CheckGizmoColor();
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
            if (useSphere)
                Gizmos.DrawSphere(Vector3.zero, radius);
            else
                Gizmos.DrawCube(Vector3.zero, new Vector3(hitboxSize.x * 2, hitboxSize.y * 2, hitboxSize.z * 2));
        }

        private void CheckGizmoColor()
        {
            switch (_state)
            {
                case ColliderState.Closed:
                    Gizmos.color = inactiveColor;
                    break;
                case ColliderState.Open:
                    Gizmos.color = collisionOpenColor;
                    break;
                case ColliderState.Colliding:
                    Gizmos.color = collidingColor;
                    break;
            }
        }
    }
}
