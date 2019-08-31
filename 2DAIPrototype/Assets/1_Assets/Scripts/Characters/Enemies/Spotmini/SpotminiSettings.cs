using UnityEngine;

namespace Platformer.Scripts.Characters.Enemies.Spotmini
{
    [CreateAssetMenu(menuName = "Character/Spotmini/Settings", fileName = "SpotminiData")]
    public class SpotminiSettings : ScriptableObject
    {
        [SerializeField]
        private float moveSpeed = 6f;
        [SerializeField]
        private bool useAi = false;

        [Header("Jump Variables")]
        [SerializeField]
        private float maxJumpHeight = 4;
        [SerializeField]
        private float minJumpHeight = 1;
        [SerializeField]
        private float timeToJumpApex = .4f;

        private float accelerationTimeAirborne = .2f;
        private float accelerationTimeGrounded = .1f;

        public float MoveSpeed { get { return moveSpeed; } }
        public bool UseAi { get { return useAi; } }
        public float Gravity { get; private set; }
        public float MaxJumpVelocity { get; private set; }
        public float MinJumpVelocity { get; private set; }
        public float AccelerationTimeAirborne { get { return accelerationTimeAirborne; } }
        public float AccelerationTimeGrounded { get { return accelerationTimeGrounded; } }

        public SpotminiSettings()
        {
            Gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
            MaxJumpVelocity = Mathf.Abs(Gravity) * timeToJumpApex;
            MinJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(Gravity) * minJumpHeight);
        }
    }
}
