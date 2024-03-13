using System.IO;
using UnityEngine;

namespace Architecture.Save {
  public class SavedData : ISavedData{
    private Data _data;

    public SavedData() {
      Initialize();
    }

    public Data GetSaveData() {
      return _data;
    }

    public void SaveGame() {
#if !UNITY_EDITOR
      string filePath = Application.persistentDataPath + Constants.Constants.Paths.DATA_PATH;
#else
      string filePath = Application.dataPath + Constants.Constants.Paths.DATA_PATH;
#endif
      string json = JsonUtility.ToJson(_data, true);
      File.WriteAllText(filePath, json);
    }

    private void Initialize() {
#if !UNITY_EDITOR
      string filePath = Application.persistentDataPath + Constants.Constants.Paths.DATA_PATH;
#else
      string filePath = Application.dataPath + Constants.Constants.Paths.DATA_PATH;
#endif
      if (File.Exists(filePath)) {
        string jsonAllText = File.ReadAllText(filePath);
        _data = JsonUtility.FromJson<Data>(jsonAllText);
        return;
      }

      _data = new Data();
      string json = JsonUtility.ToJson(_data, true);
      File.WriteAllText(filePath, json);
    }
  }
}