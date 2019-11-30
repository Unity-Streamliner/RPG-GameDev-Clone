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
        [SerializeField] float fadeOutTime = 3f;
        [SerializeField] float fadeWaitTime = 2f;
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
            print("fade out " + fader);
            yield return fader.FadeOut(fadeOutTime);
            DontDestroyOnLoad(this.gameObject);
            print("re-load new scene");
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            print("scene has been reloaded");
            Fader faderOtherScene = FindObjectOfType<Fader>();
            Portal portal = GetOtherPortal();
            UpdatePlayer(portal);
            yield return new WaitForSeconds(fadeWaitTime);
            yield return faderOtherScene.FadeIn(fadeInTime);
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
            player.GetComponent<NavMeshAgent>().Warp(portal.spawnPoint.transform.position);
            player.transform.rotation = portal.spawnPoint.transform.rotation;

        }
    }
}
