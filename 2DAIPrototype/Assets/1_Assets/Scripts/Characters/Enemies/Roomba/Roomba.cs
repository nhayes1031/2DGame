using Platformer.Scripts.CameraManagement;
using UnityEngine;

namespace Platformer.Scripts.Characters.Enemies.Roomba
{
    [RequireComponent(typeof(RoombaRaycastController))]
    public class Roomba : MonoBehaviour, IControllable
    {
        [SerializeField] private RoombaSettings roombaSettings;
        [SerializeField] private GameObject playerPrefab;

        private IInput roombaInput;
        private RoombaMovement roombaMovement;
        private RoombaRaycastController roombaRaycastController;

        private void Awake()
        {
            roombaRaycastController = GetComponent<RoombaRaycastController>();
            roombaInput = roombaSettings.UseAi ? new RoombaAIInput(roombaRaycastController) as IInput : new ControllerInput();
            roombaMovement = new RoombaMovement(roombaInput, transform, roombaSettings, roombaRaycastController);
        }

        private void Update()
        {
            roombaInput.ReadInput();
            roombaMovement.Tick();

            if (roombaInput.PossessionPressed)
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

            roombaInput = new RoombaAIInput(roombaRaycastController);
            roombaMovement = new RoombaMovement(roombaInput, transform, roombaSettings, roombaRaycastController);
        }

        public void Possessed()
        {
            roombaInput = new ControllerInput();
            roombaMovement = new RoombaMovement(roombaInput, transform, roombaSettings, roombaRaycastController);
        }
    }
}
