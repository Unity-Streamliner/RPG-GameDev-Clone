using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.AI;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour 
    {

        enum DestinationIndentifier
        {
            A, B, C, D, E
        }

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIndentifier destination;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeWaitTime = 0.5f;
        [SerializeField] float fadeInTime = 2f;
        
        private void OnTriggerEnter(Collider other) {
            print("Portal trigged");
            if (other.tag == "Player") StartCoroutine(Transition());
        }

        private IEnumerator Transition() 
        {
            if (sceneToLoad < 0) 
            {
                Debug.LogError("Scene to load not set.");
                yield break;
            }
            DontDestroyOnLoad(this.gameObject);

            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);

            yield return SceneManager.LoadSceneAsync(sceneToLoad);


            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            yield return new WaitForSeconds(fadeWaitTime);

            yield return fader.FadeIn(fadeInTime);
            Destroy(gameObject);
        }

        private Portal GetOtherPortal() 
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;
                return portal;
            }
            return null;
        }
        
        private void UpdatePlayer(Portal otherPortal) 
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;

        }
    }
}
