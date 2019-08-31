using Cinemachine;
using UnityEngine;

namespace Platformer.Scripts.CameraManagement
{
    public class CameraManager : MonoBehaviour
    {
        private CinemachineVirtualCamera cam;

        public void Start()
        {
            cam = gameObject.GetComponentInChildren<CinemachineVirtualCamera>();
        }

        public void Follow(Transform unit)
        {
            cam.Follow = unit;
        }
    }
}
