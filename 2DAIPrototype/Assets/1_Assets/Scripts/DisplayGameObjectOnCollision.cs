using Platformer.SceneManagement;
using UnityEngine;

public class DisplayGameObjectOnCollision : MonoBehaviour {

    [SerializeField] private GameObject visual;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            visual.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                SavingWrapper savingWrappper = FindObjectOfType<SavingWrapper>();
                savingWrappper.Save();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            visual.SetActive(false);
        }
    }
}
