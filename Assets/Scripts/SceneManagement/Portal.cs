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
        [SerializeField] float fadeOutTime = 0.5f;
        [SerializeField] float fadeWaitTime = 0.5f;
        [SerializeField] float fadeInTime = 1f;
        
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
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);

            // Save current level
            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
            wrapper.Save();

            DontDestroyOnLoad(this.gameObject);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            yield return new WaitForSeconds(fadeWaitTime);

            // Load current level
            wrapper.Load();

            Portal portal = GetOtherPortal();
            UpdatePlayer(portal);

            wrapper.Save();
            
            yield return fader.FadeIn(fadeInTime);
            Destroy(this.gameObject);
        }

        private Portal GetOtherPortal() 
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != this.destination) continue;
                return portal;
            }
            return null;
        }
        
        private void UpdatePlayer(Portal portal) 
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.GetComponent<NavMeshAgent>().Warp(portal.spawnPoint.transform.position);
            player.transform.rotation = portal.spawnPoint.transform.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;

        }
    }
}
