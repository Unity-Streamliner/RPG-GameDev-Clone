using UnityEngine;

namespace RPG.Saving
{

    public class SaveableEntity : MonoBehaviour
    {
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
    }
}