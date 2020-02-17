using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using System.Collections.Generic;
using System;

namespace RPG.Saving
{

    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {

        [SerializeField] string uniqueIdentifier = "";
        static Dictionary<string, SaveableEntity> globalLookup = new Dictionary<string, SaveableEntity>();

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

            if (string.IsNullOrEmpty(property.stringValue) || !IsUnique(property.stringValue))
            {
                property.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }

            globalLookup[property.stringValue] = this;

            Debug.Log("Editor causes this Update");
        }

        private bool IsUnique(string candidate)
        {
            if (!globalLookup.ContainsKey(candidate)) return true;

            // check if we are point at the same entity
            if (globalLookup[candidate] == this) return true;

            // if item has been removed
            if (globalLookup[candidate] == null)
            {
                globalLookup.Remove(candidate);
                return true;
            }

            if (globalLookup[candidate].GetUniqueIdentifier() != candidate)
            {
                globalLookup.Remove(candidate);
                return true;
            }

            return false;
        }
    }
}