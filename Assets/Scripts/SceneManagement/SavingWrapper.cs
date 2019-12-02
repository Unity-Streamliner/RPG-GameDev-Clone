using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement
{
	public class SavingWrapper : MonoBehaviour
	{
        const string defaultSaveFile = "save";
        [SerializeField] float FadeInTime = 0.2f;

        IEnumerator Start()
        {
            yield return new WaitForSeconds(1);
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
            yield return fader.FadeIn(FadeInTime);
        }

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

        public void Load()
        {
            // Call to saving system
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }
    }
}
