using Platformer.Scripts.CameraManagement;
using UnityEngine;

namespace Platformer.Scripts.Characters.Enemies.Spotmini
{
    [RequireComponent(typeof(SpotminiRaycastController))]
    public class Spotmini : MonoBehaviour, IControllable
    {
        [SerializeField] private SpotminiSettings spotminiSettings;
        [SerializeField] private GameObject playerPrefab;

        private IInput spotminiInput;
        private SpotminiMovement spotminiMovement;
        private SpotminiRaycastController spotminiRaycastController;

        private void Awake()
        {
            spotminiRaycastController = GetComponent<SpotminiRaycastController>();
            spotminiInput = spotminiSettings.UseAi ? new SpotminiAIInput(spotminiRaycastController) as IInput : new ControllerInput();
            spotminiMovement = new SpotminiMovement(spotminiInput, transform, spotminiSettings, spotminiRaycastController);
        }

        private void Update()
        {
            spotminiInput.ReadInput();
            spotminiMovement.Tick();

            if (spotminiInput.PossessionPressed)
            {
                Exorcised();
            }

            if (spotminiInput.JumpPressed)
            {
                spotminiMovement.OnJumpInputDown();
            }
        }

        public void Exorcised()
        {
            GameObject newObj = Instantiate(playerPrefab, transform.position, transform.rotation);
            GameObject camera = GameObject.Find("CameraManager");
            CameraManager cm = camera.GetComponent<CameraManager>();
            cm.Follow(newObj.transform);

            spotminiInput = new SpotminiAIInput(spotminiRaycastController);
            spotminiMovement = new SpotminiMovement(spotminiInput, transform, spotminiSettings, spotminiRaycastController);
        }

        public void Possessed()
        {
            spotminiInput = new ControllerInput();
            spotminiMovement = new SpotminiMovement(spotminiInput, transform, spotminiSettings, spotminiRaycastController);
        }
    }
}
