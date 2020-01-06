using UnityEngine;
namespace RPG.Saving
{
    public class SavingWrapper : MonoBehaviour
    {
        [SerializeField] const string defaultSaveFile = "save";

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                GetComponent<SavingSystem>().Save(defaultSaveFile);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                GetComponent<SavingSystem>().Load(defaultSaveFile);
            }
           
        }
    }

}

