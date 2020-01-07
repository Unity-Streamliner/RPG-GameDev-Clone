using UnityEngine;
using System.IO;
using System.Text;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        public void Save(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Saving to " + path);
            FileStream stream = File.Open(path, FileMode.OpenOrCreate);
            byte[] bytes = Encoding.UTF8.GetBytes("¡Hola Mundo!");
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
        }

        public void Load(string saveFile)
        {
            print("Loading from " + GetPathFromSaveFile(saveFile));
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }

}

