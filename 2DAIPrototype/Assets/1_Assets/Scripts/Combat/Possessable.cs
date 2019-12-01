using Platformer.Scripts.CameraManagement;
using Platformer.Scripts.Characters.Behaviours;
using SA;
using UnityEngine;

public class Possessable : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;

    private StateManager sm;
    private void Awake()
    {
        sm = GetComponent<StateManager>();
    }

    public void Possessed()
    {
        sm.enabled = false;
        tag = "Player";
        gameObject.AddComponent<HardwareInputHandler>();
    }

    public void Exorcised()
    {
        GameObject newPlayer = Instantiate(playerPrefab, transform.position, transform.rotation);
        CameraManager cm = GameObject.Find("CameraManager").GetComponent<CameraManager>();
        cm.Follow(newPlayer.transform);

        sm.enabled = true;

        tag = "Untagged";
    }
}
