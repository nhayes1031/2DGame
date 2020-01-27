using System;

namespace Assets._1_Assets._1_Test.Scripts
{
    [Serializable]
    public class PlayerData
    {
        #region Private Variables
        private float maxSpeed = 7f;
        private float fallingMaxSpeed = 4f;

        private float dashSpeed = 10f;
        private float dashDuration = .35f;

        private float jumpSpeed = 12f;
        private int allowedJumps = 1;
        private int currentJumps = 0;

        private int allowedDashes = 0;
        private int currentDashes = 0;
        #endregion

        #region Properties
        public int CurrentJumps { get => currentJumps; private set => currentJumps = value; }
        public int AllowedJumps { get => allowedJumps; private set => allowedJumps = value; }
        public int AllowedDashes { get => allowedDashes; private set => allowedDashes = value; }
        public int CurrentDashes { get => currentDashes; private set => currentDashes = value; }
        public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
        public float FallingMaxSpeed { get => fallingMaxSpeed; set => fallingMaxSpeed = value; }
        public float DashSpeed { get => dashSpeed; set => dashSpeed = value; }
        public float DashDuration { get => dashDuration; set => dashDuration = value; }
        public float JumpSpeed { get => jumpSpeed; set => jumpSpeed = value; }
        #endregion

        #region Accessors
        public void IncrementCurrentJumps()
        {
            currentJumps++;
        }

        public void ResetCurrentJumps()
        {
            currentJumps = 0;
        }

        public void IncrementAllowedJumps()
        {
            allowedJumps++;
        }

        public void ResetAllowedJumps()
        {
            allowedJumps = 1;
        }

        public void IncrementCurrentDashes()
        {
            currentDashes++;
        }

        public void ResetCurrentDashes()
        {
            currentDashes = 0;
        }

        public void IncrementAllowedDashes()
        {
            allowedDashes++;
        }

        public void ResetAllowedDashes()
        {
            allowedDashes = 0;
        }
        #endregion
    }
}
