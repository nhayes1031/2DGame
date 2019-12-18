using UnityEngine;

namespace Platformer.Scripts.Characters.Nanoobs
{
    public class NanoobAiming
    {
        private readonly IInput nanoobInput;
        private readonly Transform reticleToAim;
        private readonly Transform origin;
        private readonly Nanoob nanoob;

        public NanoobAiming(IInput nanoobInput, Transform reticleToAim, Transform origin, Nanoob nanoob)
        {
            this.nanoobInput = nanoobInput;
            this.reticleToAim = reticleToAim;
            this.origin = origin;
            this.nanoob = nanoob;
        }

        public void Tick()
        {
            MoveReticle();
        }

        private void MoveReticle()
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - origin.position;
            difference.Normalize();
            float rotz = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            reticleToAim.rotation = Quaternion.Euler(0, 0, rotz);
        }
    }
}