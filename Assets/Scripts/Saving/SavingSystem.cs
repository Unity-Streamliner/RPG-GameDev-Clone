using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        private string _lastSceneBuildIndexKey = "lastSceneBuildIndex";

        public IEnumerator LoadLastScene(string saveFile)
        {
            // 1. get state
            Dictionary<string, object> states = LoadFile(saveFile);
            if (states.ContainsKey(_lastSceneBuildIndexKey))
            {
                int buildIndex = (int)states[_lastSceneBuildIndexKey];
                if (buildIndex != SceneManager.GetActiveScene().buildIndex)
                {
                    // 2. load last scene
                    yield return SceneManager.LoadSceneAsync(buildIndex);
                }
            }
            // 3. restore state
            RestoreState(states);
        }

        public void Save(string saveFile)
        {
            Dictionary<string, object> state = LoadFile(saveFile);
            CaptureState(state);
            SaveFile(saveFile, state);
        }

        public void Load(string saveFile)
        {
            //string path = GetPathFromSaveFile(saveFile);
            //print("Loading from " + path);
            //using (FileStream stream = File.Open(path, FileMode.Open))
            //{
            //    BinaryFormatter formatter = new BinaryFormatter();
            //    RestoreState(formatter.Deserialize(stream));
            //}
            RestoreState(LoadFile(saveFile));
        }

        private void SaveFile(string saveFile, object state)
        {
            string path = GetPathFromSaveFile(saveFile);
            using (FileStream stream = File.Open(path, FileMode.OpenOrCreate))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        private Dictionary<string, object> LoadFile(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            if (!File.Exists(path)) return new Dictionary<string, object>();
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }

        private void CaptureState(Dictionary<string, object> state)
        {
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            }
            state[_lastSceneBuildIndexKey] = SceneManager.GetActiveScene().buildIndex;
        }

        private void RestoreState(Dictionary<string, object> state)
        {
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                string id = saveable.GetUniqueIdentifier();
                if (state.ContainsKey(id))
                {
                    saveable.RestoreState(state[id]);
                }
            }
        }

        private void SetPlayerTransform(Vector3 position)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = position;
        }

        private byte[] SerializeVector(Vector3 vector)
        {
            byte[] vectorBytes = new byte[3 * 4];
            BitConverter.GetBytes(vector.x).CopyTo(vectorBytes, 0);
            BitConverter.GetBytes(vector.y).CopyTo(vectorBytes, 4);
            BitConverter.GetBytes(vector.z).CopyTo(vectorBytes, 8);
            return vectorBytes;
        }

        private Vector3 DesirializeVector(byte[] buffer)
        {
            Vector3 vector = Vector3.zero;
            vector.x = BitConverter.ToSingle(buffer, 0);
            vector.y = BitConverter.ToSingle(buffer, 4);
            vector.z = BitConverter.ToSingle(buffer, 8);
            return vector;
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }

}

