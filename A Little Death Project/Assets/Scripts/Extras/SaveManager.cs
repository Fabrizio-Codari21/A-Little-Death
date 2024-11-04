using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager
{
    public static List<SaveInfo> allSaves;

    public static void SaveGame(SaveInfo saveInfo, int indexOfSave)
    {
        var save = JsonUtility.ToJson(saveInfo);
        var filePath = Application.persistentDataPath + $"/SavedGame{indexOfSave}.json";

        System.IO.File.WriteAllText(filePath, save);
        Debug.Log($"Saved game in slot {indexOfSave}: {saveInfo}");

        if (allSaves[indexOfSave - 1] != default) allSaves[indexOfSave - 1] = saveInfo;
    }

    public static void LoadGame(int indexOfSave)
    {
        var filePath = Application.persistentDataPath + $"/SavedGame{indexOfSave}.json";
        var data = JsonUtility.FromJson<SaveInfo>(System.IO.File.ReadAllText(filePath));
    }

    public static void GetSavedGames()
    {
        allSaves[0] = JsonUtility.FromJson<SaveInfo>
                      (System.IO.File.ReadAllText
                      (Application.persistentDataPath + $"/SavedGame1.json"));

        allSaves[1] = JsonUtility.FromJson<SaveInfo>
                      (System.IO.File.ReadAllText
                      (Application.persistentDataPath + $"/SavedGame2.json"));

        allSaves[2] = JsonUtility.FromJson<SaveInfo>
                      (System.IO.File.ReadAllText
                      (Application.persistentDataPath + $"/SavedGame3.json"));
    }

    public static bool HasSavedGame(SaveInfo savedInfo)
    {
        foreach (var save in allSaves)
        {
            if(save == savedInfo)
            {
                return true;
            }
        }

        return false;
    }

    public static int GetSavedGameIndex(SaveInfo savedInfo)
    {
        foreach (var save in allSaves)
        {
            if (save == savedInfo)
            {
                return allSaves.IndexOf(save) + 1;
            }
        }

        Debug.Log("The requested save index does not exist");
        return -1;
    }
}

[System.Serializable]
public class SaveInfo
{
    public int savingSpot;
    public int playerHealth;
}
