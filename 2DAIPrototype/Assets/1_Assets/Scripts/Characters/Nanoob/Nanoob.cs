using Platformer.Saving;
using Platformer.Scripts.Abilities.Mindcontrol;
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

        private void SomethingWasHit(string tag)
        {
            if (tag == "Controllable")
            {
                Destroy(gameObject);
            }
        }

        public void Instantiate(Quaternion rotation)
        {
            GameObject newObj = Instantiate(bulletPrefab, firePoint.position, rotation);
            Mindcontrol mc = newObj.GetComponent<Mindcontrol>();
            mc.iHitSomething = SomethingWasHit;
            mc.SetDirection((Vector2)firePoint.position - (Vector2)transform.position);
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
