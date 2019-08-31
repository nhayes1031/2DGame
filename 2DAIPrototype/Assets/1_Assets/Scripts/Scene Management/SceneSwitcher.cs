// using Platformer.Saving;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.SceneManagement
{
    public class SceneSwitcher : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D, E, F, G   
        }

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 2f;
        [SerializeField] float fadeInTime = 1f;
        [SerializeField] float fadeWaitTime = 0.5f;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if (sceneToLoad < 0)
            {
                Debug.LogError("Scene to load not set.");
                yield break;
            }

            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();

            yield return fader.FadeOut(fadeOutTime);

            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            SceneSwitcher otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            yield return new WaitForSeconds(fadeWaitTime);

            yield return fader.FadeIn(fadeInTime);

            Destroy(gameObject);
        }

        private void UpdatePlayer(SceneSwitcher otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            // TODO: Figure out what side to flip the player to
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private SceneSwitcher GetOtherPortal()
        {
            foreach (SceneSwitcher switcher in FindObjectsOfType<SceneSwitcher>())
            {
                if (switcher == this) continue;
                if (switcher.destination != destination) continue;

                return switcher;
            }

            return null;
        }
    }
}
