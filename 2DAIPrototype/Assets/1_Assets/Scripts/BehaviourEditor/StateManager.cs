using Platformer.Scripts.Characters.Behaviours;
using UnityEngine;

namespace SA
{
    public class StateManager : MonoBehaviour
    {
        public State currentState;

        [HideInInspector]
        public float delta;
        [HideInInspector]
        public Transform mTransform;
        [HideInInspector]
        public ColliderBehaviour coll;
        [HideInInspector]
        public float jumpTimer;
        [HideInInspector]
        public Vector2 origin;

        public GameObject target { get; private set; }

        private void Start()
        {
            mTransform = this.transform;
            coll = gameObject.GetComponentInChildren<ColliderBehaviour>();
            origin = transform.position;
        }

        private void Update()
        {
            if(currentState != null)
            {
                currentState.Tick(this);
            }
        }

        public void SetNewTarget(GameObject target)
        {
            this.target = target;
        }
    }
}
