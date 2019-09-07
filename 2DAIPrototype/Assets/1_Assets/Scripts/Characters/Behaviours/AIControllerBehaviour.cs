using Platformer.Scripts.Characters.AI;
using System;
using UnityEngine;

namespace Platformer.Scripts.Characters.Behaviours
{
    [RequireComponent(typeof(ColliderBehaviour))]
    public class AIControllerBehaviour : MonoBehaviour, IController
    {
        public State state;
        public State remainState;

        public event Action<Vector2> OnMove;
        public event Action<Vector2> OnLook;
        public event System.Action OnJump;
        public event System.Action OnJumpReleased;

        [HideInInspector]
        public ColliderBehaviour coll;
        [HideInInspector]
        public float jumpTimer;

        private void Start()
        {
            coll = gameObject.GetComponent<ColliderBehaviour>();
        }

        private void Update()
        {
            state.UpdateState(this);
        }

        public void TransitionToState(State nextState)
        {
            if (nextState != remainState)
            {
                state = nextState;
            }
        }

        public void TriggerOnMove(Vector2 move)
        {
            if (OnMove != null)
            {
                OnMove(move);
            }
        }

        public void TriggerOnJump()
        {
            if (OnJump != null)
            {
                OnJump();
            }
        }
    }
}
