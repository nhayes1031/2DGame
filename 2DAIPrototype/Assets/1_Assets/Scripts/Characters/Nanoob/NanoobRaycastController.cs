using UnityEngine;

namespace Platformer.Scripts.Characters.Nanoobs
{
    public class NanoobRaycastController : RaycastController {
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

        private float DistanceBetweenTwoPoints(Vector2 p1, Vector2 p2)
        {
            float horzDist = Mathf.Pow(p2.x - p1.x, 2);
            float vertDist = Mathf.Pow(p2.y - p1.y, 2);
            return Mathf.Sqrt(horzDist + vertDist);
        }

        public Vector2 CheckForCollisionAt(Vector2 desiredMove)
        {
            float directionX = collisions.faceDir;
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, Mathf.Abs(desiredMove.x), collisionMask);
            if (hit)
            {
                Debug.DrawRay(new Vector2(rayOrigin.x + skinWidth, rayOrigin.y), Vector2.right * directionX * hit.distance, Color.green, 10.0f);
                float distanceMinusSkinWidth = directionX * (hit.distance - skinWidth);
                Debug.Log(distanceMinusSkinWidth);

                // Move onto checking behind the end point
                Vector2 newTopLeft = new Vector2(raycastOrigins.topLeft.x + desiredMove.x, raycastOrigins.topLeft.y + desiredMove.y + skinWidth);
                Vector2 newTopRight = new Vector2(raycastOrigins.topRight.x + desiredMove.x, raycastOrigins.topRight.y + desiredMove.y + skinWidth);
                Vector2 newBottomLeft = new Vector2(raycastOrigins.bottomLeft.x + desiredMove.x, raycastOrigins.bottomLeft.y + desiredMove.y);
                Vector2 newBottomRight = new Vector2(raycastOrigins.bottomRight.x + desiredMove.x, raycastOrigins.bottomRight.y + desiredMove.y);

                RaycastHit2D hit2 = Physics2D.Raycast(newTopLeft, Vector2.right, DistanceBetweenTwoPoints(newTopLeft, newTopRight), collisionMask);
                if (hit2)
                {
                    desiredMove.x = distanceMinusSkinWidth;
                    return desiredMove;
                }

                hit2 = Physics2D.Raycast(newTopRight, Vector2.down, DistanceBetweenTwoPoints(newTopRight, newBottomRight), collisionMask);
                if (hit2)
                {
                    desiredMove.x = distanceMinusSkinWidth;
                    return desiredMove;
                }

                hit2 = Physics2D.Raycast(newBottomRight, Vector2.left, DistanceBetweenTwoPoints(newBottomRight, newBottomLeft), collisionMask);
                if (hit2)
                {
                    desiredMove.x = distanceMinusSkinWidth;
                    return desiredMove;
                }

                hit2 = Physics2D.Raycast(newBottomLeft, Vector2.up, DistanceBetweenTwoPoints(newBottomLeft, newTopLeft), collisionMask);
                if (hit2)
                {
                    desiredMove.x = distanceMinusSkinWidth;
                    return desiredMove;
                }
            }

            return desiredMove;
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