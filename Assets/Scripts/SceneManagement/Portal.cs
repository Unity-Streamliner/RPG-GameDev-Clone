using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.AI;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour 
    {

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        
        private void OnTriggerEnter(Collider other) {
            print("Portal trigged");
            if (other.tag == "Player") StartCoroutine(Transition());
        }

        private IEnumerator Transition() 
        {
            DontDestroyOnLoad(this.gameObject);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            Portal portal = GetOtherPortal();
            UpdatePlayer(portal);
            Destroy(this.gameObject);
        }

        private Portal GetOtherPortal() 
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
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
