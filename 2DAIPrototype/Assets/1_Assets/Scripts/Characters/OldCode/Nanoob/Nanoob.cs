using Platformer.Saving;
using UnityEngine;

namespace Platformer.Scripts.Characters.Nanoobs
{
    [RequireComponent(typeof(NanoobRaycastController))]
    [RequireComponent(typeof(ISaveable))]
    public class Nanoob : MonoBehaviour, ISaveable
    {
        [SerializeField] private NanoobSettings nanoobSettings;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform firePoint;

        private IInput nanoobInput;
        private NanoobMovement nanoobMovement;
        private NanoobAiming nanoobAiming;
        private NanoobRaycastController nanoobRaycastController;

        private void Awake()
        {
            nanoobInput = new ControllerInput();
            nanoobRaycastController = GetComponent<NanoobRaycastController>();
            nanoobMovement = new NanoobMovement(nanoobInput, transform, nanoobSettings, nanoobRaycastController);
            Transform reticle = transform.Find("Reticle").transform;
            nanoobAiming = new NanoobAiming(nanoobInput, reticle, transform, this);
        }

        private void Update()
        {
            nanoobInput.ReadInput();
            nanoobMovement.Tick();
            nanoobAiming.Tick();

            if (nanoobInput.JumpPressed)
            {
                nanoobMovement.OnJumpInputDown();
            }

            if (nanoobInput.JumpReleased)
            {
                nanoobMovement.OnJumpInputUp();
            }
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            transform.position = ((SerializableVector3)state).ToVector();
        }
    }
}
