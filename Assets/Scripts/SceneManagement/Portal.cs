using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour 
    {

        [SerializeField] int sceneToLoad = -1;
        
        private void OnTriggerEnter(Collider other) {
            print("Portal trigged");
            if (other.tag == "Player") StartCoroutine(Transition());
        }

        private IEnumerator Transition() 
        {
            DontDestroyOnLoad(this.gameObject);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            print("scene loaded");
            Destroy(this.gameObject);
        }

    }
}
