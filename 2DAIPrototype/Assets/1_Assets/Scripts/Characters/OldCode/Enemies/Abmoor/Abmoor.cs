using Platformer.Scripts.CameraManagement;
using UnityEngine;

namespace Platformer.Scripts.Characters.Enemies.Abmoor
{
    [RequireComponent(typeof(AbmoorRaycastController))]
    public class Abmoor : MonoBehaviour, IControllable
    {
        [SerializeField] private AbmoorSettings abmoorSettings;
        [SerializeField] private GameObject playerPrefab;

        private IInput abmoorInput;
        private AbmoorMovement abmoorMovement;
        private AbmoorRaycastController abmoorRaycastController;

        private void Awake()
        {
            abmoorRaycastController = GetComponent<AbmoorRaycastController>();
            abmoorInput = abmoorSettings.UseAi ? new AbmoorAIInput() as IInput : new ControllerInput();
            abmoorMovement = new AbmoorMovement(abmoorInput, transform, abmoorSettings, abmoorRaycastController);
        }

        private void Update()
        {
            abmoorInput.ReadInput();
            abmoorMovement.Tick();

            if (abmoorInput.PossessionPressed)
            {
                Exorcised();
            }
        }

        public void Exorcised()
        {
            GameObject newObj = Instantiate(playerPrefab, transform.position, transform.rotation);
            GameObject camera = GameObject.Find("CameraManager");
            CameraManager cm = camera.GetComponent<CameraManager>();
            cm.Follow(newObj.transform);

            abmoorInput = new AbmoorAIInput();
            abmoorMovement = new AbmoorMovement(abmoorInput, transform, abmoorSettings, abmoorRaycastController);
        }

        public void Possessed()
        {
            abmoorInput = new ControllerInput();
            abmoorMovement = new AbmoorMovement(abmoorInput, transform, abmoorSettings, abmoorRaycastController);
        }
    }
}
