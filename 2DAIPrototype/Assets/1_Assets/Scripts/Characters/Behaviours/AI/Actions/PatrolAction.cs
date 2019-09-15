﻿using MyBox;
using Platformer.Scripts.Characters.Behaviours;
using UnityEngine;

namespace Platformer.Scripts.Characters.AI
{
    [CreateAssetMenu(menuName = "AI/Actions/Patrol")]
    public class PatrolAction : Action
    {
        [SerializeField]
        [Range(0, 1)]
        public float speed;
        [SerializeField]
        public bool checkForLedge = false;
        [SerializeField]
        public bool territorial = false;
        [ConditionalField("territorial")]
        public float roamDistance = 100;
        [SerializeField]
        public LayerMask collisionMask;

        private int direction = 1;

        public override void Act(AIControllerBehaviour controller)
        {
            CheckDirection(controller);
            Patrol(controller);
        }

        private void CheckDirection(AIControllerBehaviour controller)
        {
            CheckForWallCollisions(controller);
            if (checkForLedge)
            {
                CheckForLedge(controller);
            }
            if (territorial)
            {
                CheckForMaximumRoamDistance(controller);
            }
        }

        private void CheckForWallCollisions(AIControllerBehaviour controller)
        {
            if (controller.coll.collisions.right || controller.coll.collisions.left)
            {
                FlipDirection(controller);
            }
        }

        private void CheckForLedge(AIControllerBehaviour controller)
        {
            Vector2 rayOrigin = controller.coll.collisions.faceDir == 1 ? controller.coll.raycastOrigins.bottomRight : controller.coll.raycastOrigins.bottomLeft;
            rayOrigin.x += controller.coll.collisions.faceDir * 0.25f;

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, 0.5f, collisionMask);
            Debug.DrawRay(rayOrigin, Vector2.down * 0.25f);
            if (hit.collider == null)
            {
                FlipDirection(controller);
            }
        }

        private void CheckForMaximumRoamDistance(AIControllerBehaviour controller)
        {
            Vector2 leftMost = controller.origin;
            leftMost.x -= roamDistance;
            Vector2 rightMost = controller.origin;
            rightMost.x += roamDistance;
            Debug.DrawRay(leftMost, Vector2.down, Color.green, 10);
            Debug.DrawRay(rightMost, Vector2.down, Color.green, 10);

            if (Vector2.Distance(controller.gameObject.transform.position, controller.origin) >= roamDistance)
            {
                direction = IsLeft(controller.gameObject.transform.position, controller.origin) ? 1 : -1;
            }
        }

        private bool IsLeft(Vector2 a, Vector2 b)
        {
            return -a.x * b.y + a.y * b.x < 0;
        }

        private void FlipDirection(AIControllerBehaviour controller)
        {
            direction = controller.coll.collisions.faceDir * -1;
        }

        private void Patrol(AIControllerBehaviour controller)
        {
            Vector2 move = Vector2.zero;
            move.x = speed * direction;
            controller.TriggerOnMove(move);
        }
    }
}