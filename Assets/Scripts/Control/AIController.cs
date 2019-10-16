using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour 
    {
        [SerializeField] float chaseDistance = 5f;

        void Update()
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (Vector3.Distance(transform.position, player.transform.position) < chaseDistance) {
                Debug.Log("Chase " + player.name);
            }
        }
    }
}