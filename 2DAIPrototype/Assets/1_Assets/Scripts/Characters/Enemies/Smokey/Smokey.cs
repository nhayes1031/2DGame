using Platformer.Scripts.CameraManagement;
using UnityEngine;

namespace Platformer.Scripts.Characters.Enemies.Smokey
{
    [RequireComponent(typeof(SmokeyRaycastController))]
    public class Smokey : MonoBehaviour, IControllable
    {

        [SerializeField] private SmokeySettings smokeySettings;
        [SerializeField] private GameObject playerPrefab;

        private IInput smokeyInput;
        private SmokeyMovement smokeyMovement;
        private SmokeyRaycastController smokeyRaycastController;

        private void Awake()
        {
            smokeyRaycastController = GetComponent<SmokeyRaycastController>();
            smokeyInput = smokeySettings.UseAi ? new SmokeyAIInput(smokeyRaycastController) as IInput : new ControllerInput();
            smokeyMovement = new SmokeyMovement(smokeyInput, transform, smokeySettings, smokeyRaycastController);
            if (smokeySettings.UseAi)
            {
                ((SmokeyAIInput)smokeyInput).Init(transform.position);
            }
        }

        private void Update()
        {
            smokeyInput.ReadInput();
            smokeyMovement.Tick();

            if (smokeyInput.PossessionPressed)
            {
                Exorcised();
            }

            if (smokeyInput.JumpPressed)
            {
                smokeyMovement.OnJumpInputDown();
            }
        }

        public void Exorcised()
        {
            GameObject newObj = Instantiate(playerPrefab, transform.position, transform.rotation);
            GameObject camera = GameObject.Find("CameraManager");
            CameraManager cm = camera.GetComponent<CameraManager>();
            cm.Follow(newObj.transform);

            smokeyInput = new SmokeyAIInput(smokeyRaycastController);
            smokeyMovement = new SmokeyMovement(smokeyInput, transform, smokeySettings, smokeyRaycastController);
        }

        public void Possessed()
        {
            smokeyInput = new ControllerInput();
            smokeyMovement = new SmokeyMovement(smokeyInput, transform, smokeySettings, smokeyRaycastController);
        }
    }
}
