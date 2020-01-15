using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Saving
{

    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {

        [SerializeField] string uniqueIdentifier = ""; 

        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

        public object CaptureState()
        {
            print("CaptureState for GetUniqueIdentifier = " + GetUniqueIdentifier());
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            print("Restoring state for " + GetUniqueIdentifier());
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = (state as SerializableVector3).ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
            
        }

        void Update()
        {
            if (Application.IsPlaying(gameObject)) return;
            // if path is empty then we are in prefab
            if (string.IsNullOrEmpty(gameObject.scene.path)) return;

            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty property = serializedObject.FindProperty("uniqueIdentifier");

            if (string.IsNullOrEmpty(property.stringValue))
            {
                property.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }

            Debug.Log("Editor causes this Update");
        }
    }
}