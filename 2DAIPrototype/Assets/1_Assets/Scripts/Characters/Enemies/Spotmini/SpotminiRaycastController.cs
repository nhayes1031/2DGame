using UnityEngine;

namespace Platformer.Scripts.Characters.Enemies.Spotmini
{
    public class SpotminiRaycastController : RaycastController
    {
        public CollisionInfo collisions;

        public override void Start()
        {
            base.Start();
            collisions.faceDir = 1;
        }

        public Vector2 Move(Vector2 moveAmount, bool standingOnPlatform = false)
        {
            UpdateRaycastOrigins();
            collisions.Reset();

            if (moveAmount.x != 0)
            {
                collisions.faceDir = (int)Mathf.Sign(moveAmount.x);
            }

            HorizontalCollisions(ref moveAmount);

            if (moveAmount.y != 0)
            {
                VerticalCollisions(ref moveAmount);
            }

            if (standingOnPlatform)
            {
                collisions.below = true;
            }

            return moveAmount;
        }

        public int OnEdge() {
            if (collisions.left || collisions.right)
            {
                return collisions.faceDir;
            }

            Vector2 rayOrigin = collisions.faceDir == 1 ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
            rayOrigin.x += collisions.faceDir * skinWidth * 2;

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, 5f, collisionMask);
            if (hit.collider == null)
            {
                return collisions.faceDir;
            }

            return 0;
        }

        public bool ShouldIJump() {
            bool shouldJump = false;

            Vector2 rayOrigin = collisions.faceDir == 1 ? raycastOrigins.topRight : raycastOrigins.topLeft;

            RaycastHit2D ledgeCheck = Physics2D.Raycast(rayOrigin, new Vector2(collisions.faceDir * 1.5f, 2), 2.5f, collisionMask);
            RaycastHit2D wallCheck = Physics2D.Raycast(rayOrigin, Vector2.right * collisions.faceDir, 1.5f, collisionMask);
            Debug.DrawRay(rayOrigin, new Vector2(collisions.faceDir * 1.5f, 2), Color.red);
            Debug.DrawRay(rayOrigin, Vector2.right * collisions.faceDir * 1.5f, Color.red);
            shouldJump = !ledgeCheck && wallCheck;

            return shouldJump;
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
            int directionY = (int)Mathf.Sign(moveAmount.y);
            float rayLength = Mathf.Abs(moveAmount.y) + skinWidth;

            for (int i = 0; i < verticalRayCount; i++)
            {
                Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
                rayOrigin += Vector2.right * (verticalRaySpacing * i + moveAmount.x);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

                Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.green);

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
