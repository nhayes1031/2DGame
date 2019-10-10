using Platformer.Scripts.Physics;
using UnityEngine;

namespace Platformer.Scripts.Characters.Behaviours
{
    public class ColliderBehaviour : RaycastController
    {
        [SerializeField] public FloatReference gravity;

        public CollisionInfo collisions;
        private Vector2 velocity;

        public Vector2 Velocity
        {
            get { return velocity; }
            set
            {
                velocity.x = value.x;
            }
        }

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
            collisions.faceDir = 1;
            velocity = Vector2.zero;
        }

        private void FixedUpdate()
        {
            ApplyGravity();
            Move(velocity * Time.deltaTime);

            if (collisions.above || collisions.below)
            {
                velocity.y = 0;
            }
        }

        private void ApplyGravity()
        {
            velocity.y -= gravity;
        }

        public void AddImpulseForce(Vector2 force)
        {
            velocity += force;
        }

        public void MultipleVelocity(Vector2 multiplier)
        {
            velocity *= multiplier;
        }

        private void Move(Vector2 desiredMove, bool standingOnPlatform = false)
        {
            UpdateRaycastOrigins();
            collisions.Reset();

            if (desiredMove.x != 0)
            {
                collisions.faceDir = (int)Mathf.Sign(desiredMove.x);
            }

            HorizontalCollisions(ref desiredMove);

            VerticalCollisions(ref desiredMove);

            if (standingOnPlatform)
            {
                collisions.below = true;
            }

            transform.parent.transform.Translate(desiredMove);
            FlipTransformAlongZ();
        }

        private void FlipTransformAlongZ()
        {
            Vector3 theScale = transform.parent.transform.localScale;
            if (theScale.x > 0 && collisions.faceDir == -1)
            {
                theScale.x *= collisions.faceDir;
                transform.parent.transform.localScale = theScale;
            } else if (theScale.x < 0 && collisions.faceDir == 1)
            {
                theScale.x *= -collisions.faceDir;
                transform.parent.transform.localScale = theScale;
            }
        }

        private void HorizontalCollisions(ref Vector2 moveAmount)
        {
            float directionX = collisions.faceDir;
            float rayLength = Mathf.Abs(moveAmount.x) + skinWidth;

            if (Mathf.Abs(moveAmount.x) < skinWidth)
            {
                rayLength = 2 * skinWidth;
            }

            for (int i = 0; i < horizontalRayCount; i++)
            {
                Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
                rayOrigin += Vector2.up * (horizontalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

                Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

                if (hit)
                {
                    if (hit.distance == 0) continue;

                    moveAmount.x = (hit.distance - skinWidth) * directionX;
                    rayLength = hit.distance;

                    collisions.left = directionX == -1;
                    collisions.right = directionX == 1;
                }
            }
        }

        private void VerticalCollisions(ref Vector2 moveAmount)
        {
            float directionY = Mathf.Sign(moveAmount.y);
            float rayLength = Mathf.Abs(moveAmount.y) + skinWidth;

            for (int i = 0; i < verticalRayCount; i++)
            {
                Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
                rayOrigin += Vector2.right * (verticalRaySpacing * i + moveAmount.x);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

                Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

                if (hit)
                {
                    if (hit.collider.tag == "Through")
                    {
                        if (directionY == 1 || hit.distance == 0)
                        {
                            continue;
                        }
                    }

                    moveAmount.y = (hit.distance - skinWidth) * directionY;
                    rayLength = hit.distance;

                    collisions.below = directionY == -1;
                    collisions.above = directionY == 1;
                }
            }
        }

        public struct CollisionInfo
        {
            public bool above, below;
            public bool left, right;
            public int faceDir;

            public void Reset()
            {
                above = below = false;
                left = right = false;
            }
        }
    }
}
