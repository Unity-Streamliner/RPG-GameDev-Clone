using UnityEngine;

namespace RPG.Saving
{

    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {

        [SerializeField] string uniqueIdentifier = ""; //System.Guid.NewGuid().ToString();

        public string GetUniqueIdentifier()
        {
            return "";
        }

        public object CaptureState()
        {
            print("CaptureState for GetUniqueIdentifier = " + GetUniqueIdentifier());
            return null;
        }

        public void RestoreState(object state)
        {
            print("Restoring state for " + GetUniqueIdentifier());
        }

        void Update()
        {
            Debug.Log("Editor causes this Update");
        }
    }
}