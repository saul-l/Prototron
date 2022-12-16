
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveDataHandler : MonoBehaviour
{
    string saveFileName;
    string saveFileString;

    SaveData saveData = new SaveData();

    private void Awake()
    {
        saveFileName = Application.persistentDataPath + "/savefile.data";

        
    }

    public void ReadSaveFile()
    {
        if (File.Exists(saveFileName))
        {
            string saveFileString = File.ReadAllText(saveFileName);
        }
    }

    public void WriteSaveFile()
    {
        File.WriteAllText(saveFileName, saveFileString);
    }
}

[System.Serializable]
public class SaveFileData
{
    public int maxPhase;
    public List<GameObject> savedWeapons;
}
