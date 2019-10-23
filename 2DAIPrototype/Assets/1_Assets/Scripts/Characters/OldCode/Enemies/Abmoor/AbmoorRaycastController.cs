using Platformer.Scripts.Physics;
using UnityEngine;

namespace Platformer.Scripts.Characters.Enemies.Abmoor
{
    public class AbmoorRaycastController : RaycastController
    {
        public CollisionInfo collisions;
        public Direction stickyDirection;

        protected override void Start()
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

            CheckForCollisions(ref moveAmount);

            DecideRotation();

            if (standingOnPlatform)
            {
                collisions.below = true;
            }

            return moveAmount;
        }

        private void DecideRotation()
        {
            WallCollisionCheck();
            OffEdgeCheck();
        }

        private void WallCollisionCheck()
        {
            if (collisions.right && stickyDirection != Direction.right)
            {
                transform.eulerAngles = new Vector3(0, 0, 90);
                stickyDirection = Direction.right;
            }
            else if (collisions.above && stickyDirection != Direction.up)
            {
                transform.eulerAngles = new Vector3(0, 0, 180);
                stickyDirection = Direction.up;
            }
            else if (collisions.left && stickyDirection != Direction.left)
            {
                transform.eulerAngles = new Vector3(0, 0, 270);
                stickyDirection = Direction.left;
            }
            else if (collisions.below && stickyDirection != Direction.down)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                stickyDirection = Direction.down;
            }
        }

        private void OffEdgeCheck()
        {
            switch (stickyDirection)
            {
                case Direction.up:
                    if (collisions.faceDir == 1)
                    {
                        if (isOffEdge(raycastOrigins.topRight, Vector2.up) && isOffEdge(raycastOrigins.topLeft, new Vector2(-.5f, .5f)))
                        {
                            stickyDirection = Direction.right;
                            transform.eulerAngles = new Vector3(0, 0, -270);
                        }
                    }
                    else
                    {
                        if (isOffEdge(raycastOrigins.topLeft, Vector2.up) && isOffEdge(raycastOrigins.topRight, new Vector2(.5f, .5f)))
                        {
                            stickyDirection = Direction.left;
                            transform.eulerAngles = new Vector3(0, 0, 270);
                        }
                    }
                    break;
                case Direction.right:
                    if (collisions.faceDir == 1)
                    {
                        if (isOffEdge(raycastOrigins.bottomRight, Vector2.right) && isOffEdge(raycastOrigins.topRight, new Vector2(.5f, .5f)))
                        {
                            stickyDirection = Direction.down;
                            transform.eulerAngles = new Vector3(0, 0, 0);
                        }
                    }
                    else
                    {
                        if (isOffEdge(raycastOrigins.topRight, Vector2.right) && isOffEdge(raycastOrigins.bottomRight, new Vector2(.5f, -.5f)))
                        {
                            stickyDirection = Direction.up;
                            transform.eulerAngles = new Vector3(0, 0, 180);
                        }
                    }
                    break;
                case Direction.down:
                    if (collisions.faceDir == 1)
                    {
                        if (isOffEdge(raycastOrigins.bottomLeft, Vector2.down) && isOffEdge(raycastOrigins.bottomRight, new Vector2(.5f, -.5f)))
                        {
                            stickyDirection = Direction.left;
                            transform.eulerAngles = new Vector3(0, 0, -90);
                        }
                    }
                    else
                    {
                        if (isOffEdge(raycastOrigins.bottomRight, Vector2.down) && isOffEdge(raycastOrigins.bottomLeft, new Vector2(-.5f, -.5f)))
                        {
                            stickyDirection = Direction.right;
                            transform.eulerAngles = new Vector3(0, 0, 90);
                        }
                    }
                    break;
                case Direction.left:
                    if (collisions.faceDir == 1)
                    {
                        if (isOffEdge(raycastOrigins.topLeft, Vector2.left) && isOffEdge(raycastOrigins.bottomLeft, new Vector2(-.5f, -.5f)))
                        {
                            stickyDirection = Direction.up;
                            transform.eulerAngles = new Vector3(0, 0, -180);
                        }
                    }
                    else
                    {
                        if (isOffEdge(raycastOrigins.bottomLeft, Vector2.left) && isOffEdge(raycastOrigins.topLeft, new Vector2(-.5f, .5f)))
                        {
                            stickyDirection = Direction.down;
                            transform.eulerAngles = new Vector3(0, 0, 0);
                        }
                    }
                    break;
            }
        }

        private bool isOffEdge(Vector2 vertex, Vector2 direction)
        {
            RaycastHit2D hit = Physics2D.Raycast(vertex, direction, 20 * skinWidth, collisionMask);
            if (hit)
            {
                return false;
            }
            return true;
        }

        private void CheckForCollisions(ref Vector2 moveAmount)
        {
            switch (stickyDirection)
            {
                case Direction.up:
                    //CheckUp(ref moveAmount);
                    if (collisions.faceDir == 1)
                    {
                        CheckLeft(ref moveAmount);
                    }
                    else
                    {
                        CheckRight(ref moveAmount);
                    }
                    break;
                case Direction.right:
                    //CheckRight(ref moveAmount);
                    if (collisions.faceDir == 1)
                    {
                        CheckUp(ref moveAmount);
                    }
                    else
                    {
                        CheckDown(ref moveAmount);
                    }
                    break;
                case Direction.down:
                    //CheckDown(ref moveAmount);
                    if (collisions.faceDir == 1)
                    {
                        CheckRight(ref moveAmount);
                    }
                    else
                    {
                        CheckLeft(ref moveAmount);
                    }
                    break;
                case Direction.left:
                    //CheckLeft(ref moveAmount);
                    if (collisions.faceDir == 1)
                    {
                        CheckDown(ref moveAmount);
                    }
                    else
                    {
                        CheckUp(ref moveAmount);
                    }
                    break;
            }
        }

        private void CheckUp(ref Vector2 moveAmount)
        {
            float rayLength = Mathf.Abs(moveAmount.x) + skinWidth;

            if (Mathf.Abs(moveAmount.x) < skinWidth)
            {
                rayLength = 2 * skinWidth;
            }

            for (int i = 0; i < horizontalRayCount; i++)
            {
                Vector2 translatedRayOrigin = raycastOrigins.topLeft + (Vector2.right * (horizontalRaySpacing * i));
                RaycastHit2D hit = Physics2D.Raycast(translatedRayOrigin, Vector2.up, rayLength, collisionMask);

                Debug.DrawRay(translatedRayOrigin, Vector2.up * rayLength, Color.white);

                if (hit)
                {
                    if (hit.distance == 0) continue;

                    moveAmount.x = (hit.distance - skinWidth) * collisions.faceDir;
                    rayLength = hit.distance;

                    if (collisions.faceDir == 1)
                    {
                        collisions.below = false;
                        collisions.above = true;
                    }
                    else
                    {
                        collisions.below = false;
                        collisions.above = true;
                    }
                }
            }
        }

        private void CheckRight(ref Vector2 moveAmount)
        {
            float rayLength = Mathf.Abs(moveAmount.x) + skinWidth;

            if (Mathf.Abs(moveAmount.x) < skinWidth)
            {
                rayLength = 2 * skinWidth;
            }

            for (int i = 0; i < horizontalRayCount; i++)
            {
                Vector2 translatedRayOrigin = raycastOrigins.bottomRight + (Vector2.up * (horizontalRaySpacing * i));
                RaycastHit2D hit = Physics2D.Raycast(translatedRayOrigin, Vector2.right, rayLength, collisionMask);

                Debug.DrawRay(translatedRayOrigin, Vector2.right * rayLength, Color.white);

                if (hit)
                {
                    if (hit.distance == 0) continue;

                    moveAmount.x = (hit.distance - skinWidth) * collisions.faceDir;
                    rayLength = hit.distance;

                    if (collisions.faceDir == 1)
                    {
                        collisions.left = false;
                        collisions.right = true;
                    }
                    else
                    {
                        collisions.left = false;
                        collisions.right = true;
                    }
                }
            }
        }

        private void CheckDown(ref Vector2 moveAmount)
        {
            float rayLength = Mathf.Abs(moveAmount.x) + skinWidth;

            if (Mathf.Abs(moveAmount.x) < skinWidth)
            {
                rayLength = 2 * skinWidth;
            }

            for (int i = 0; i < horizontalRayCount; i++)
            {
                Vector2 translatedRayOrigin = raycastOrigins.bottomLeft + (Vector2.right * (horizontalRaySpacing * i));
                RaycastHit2D hit = Physics2D.Raycast(translatedRayOrigin, Vector2.down, rayLength, collisionMask);

                Debug.DrawRay(translatedRayOrigin, Vector2.down * rayLength, Color.white);

                if (hit)
                {
                    if (hit.distance == 0) continue;

                    moveAmount.x = (hit.distance - skinWidth) * collisions.faceDir;
                    rayLength = hit.distance;

                    if (collisions.faceDir == 1)
                    {
                        collisions.below = true;
                        collisions.above = false;
                    }
                    else
                    {
                        collisions.below = true;
                        collisions.above = false;
                    }
                }
            }
        }

        private void CheckLeft(ref Vector2 moveAmount)
        {
            float rayLength = Mathf.Abs(moveAmount.x) + skinWidth;

            if (Mathf.Abs(moveAmount.x) < skinWidth)
            {
                rayLength = 2 * skinWidth;
            }

            for (int i = 0; i < horizontalRayCount; i++)
            {
                Vector2 translatedRayOrigin = raycastOrigins.bottomLeft + (Vector2.up * (horizontalRaySpacing * i));
                RaycastHit2D hit = Physics2D.Raycast(translatedRayOrigin, Vector2.left, rayLength, collisionMask);

                Debug.DrawRay(translatedRayOrigin, Vector2.left * rayLength, Color.white);

                if (hit)
                {
                    if (hit.distance == 0) continue;

                    moveAmount.x = (hit.distance - skinWidth) * collisions.faceDir;
                    rayLength = hit.distance;

                    if (collisions.faceDir == 1)
                    {
                        collisions.left = true;
                        collisions.right = false;
                    }
                    else
                    {
                        collisions.left = true;
                        collisions.right = false;
                    }
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
        public enum Direction
        {
            up,
            right,
            down,
            left
        };
    }
}
