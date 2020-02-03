using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using System.Collections.Generic;

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
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach(ISavable savable in GetComponents<ISavable>())
            {
                state[savable.GetType().ToString()] = savable.CaptureState();
            }
            return state;
            //print("CaptureState for GetUniqueIdentifier = " + GetUniqueIdentifier());
            //return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            Dictionary<string, object> stateDict = (Dictionary<string, object>)state;
            foreach (ISavable savable in GetComponents<ISavable>())
            {
                string typeString = savable.GetType().ToString();
                if (stateDict.ContainsKey(typeString))
                {
                    savable.RestoreState(stateDict[typeString]);
                }
            }
            //print("Restoring state for " + GetUniqueIdentifier());
            //GetComponent<NavMeshAgent>().enabled = false;
            //transform.position = (state as SerializableVector3).ToVector();
            //GetComponent<NavMeshAgent>().enabled = true;
            //GetComponent<ActionScheduler>().CancelCurrentAction();
            
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