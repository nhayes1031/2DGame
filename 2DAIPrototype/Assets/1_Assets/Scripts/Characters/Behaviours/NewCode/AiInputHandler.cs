using Platformer.Scripts.Characters.AI.Newcode;
using UnityEngine;

namespace Platformer.Scripts.Characters.Behaviours
{
    public class AiInputHandler : MonoBehaviour
    {
        public State state;
        public State remainState;

        [HideInInspector]
        public ColliderBehaviour coll;
        [HideInInspector]
        public float jumpTimer;
        [HideInInspector]
        public Vector2 origin;

        private void Start()
        {
            coll = gameObject.GetComponent<ColliderBehaviour>();
            origin = transform.position;
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
    }
}