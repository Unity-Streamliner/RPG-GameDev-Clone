using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement
{
	public class SavingWrapper : MonoBehaviour
	{
        const string defaultSaveFile = "save"; 
		
		void Update()
		{
			if (Input.GetKeyDown(KeyCode.L))
			{
				Load();
			} else if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
		}

        private void Load()
        {
            // Call to saving system
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        private void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }
    }
}
