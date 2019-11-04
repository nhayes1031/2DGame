using UnityEngine;

namespace Platformer.Scripts.Characters.Nanoobs
{
    public class NanoobSettings : ScriptableObject
    {
        [SerializeField]
        private float moveSpeed = 6f;

        [Header("Jump Variables")]
        [SerializeField]
        private float maxJumpHeight = 4;
        [SerializeField]
        private float minJumpHeight = 1;
        [SerializeField]
        private float timeToJumpApex = .4f;

        [Header("Dash Variables")]
        [SerializeField]
        private Vector2 dashSpeed;
        [SerializeField]
        private int dashCooldown = 1;

        [Header("Wall Climb Variables")]
        [SerializeField]
        private Vector2 wallJumpClimb;
        [SerializeField]
        private Vector2 wallJumpOff;
        [SerializeField]
        private Vector2 wallLeap;
        [SerializeField]
        private float wallSlideSpeedMax = 3;
        [SerializeField]
        private float wallStickTime = 0.25f;

        private float accelerationTimeAirborne = .2f;
        private float accelerationTimeGrounded = .1f;

        public float MoveSpeed { get { return moveSpeed; } }
        public float Gravity { get; private set; }
        public float MaxJumpVelocity { get; private set; }
        public float MinJumpVelocity { get; private set; }
        public Vector2 DashSpeed { get { return dashSpeed; } }
        public int DashCooldown { get { return dashCooldown; } }
        public Vector2 WallJumpClimb { get { return wallJumpClimb; } }
        public Vector2 WallJumpOff { get { return wallJumpOff; } }
        public Vector2 WallLeap { get { return wallLeap; } }
        public float WallSlideSpeedMax { get { return wallSlideSpeedMax; } }
        public float WallStickTime { get { return wallStickTime; } }
        public float AccelerationTimeAirborne { get { return accelerationTimeAirborne; } }
        public float AccelerationTimeGrounded { get { return accelerationTimeGrounded; } }

        public NanoobSettings()
        {
            Gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
            MaxJumpVelocity = Mathf.Abs(Gravity) * timeToJumpApex;
            MinJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(Gravity) * minJumpHeight);
        }
    }
}
