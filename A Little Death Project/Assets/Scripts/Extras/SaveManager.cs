using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveManager
{
    public static List<SaveInfo> allSaves = new();

    public static void SaveGame(this MonoBehaviour x, SaveInfo saveInfo, int indexOfSave)
    {
        var save = JsonUtility.ToJson(saveInfo);
        var filePath = Application.persistentDataPath + $"/SavedGame{indexOfSave}.json";

        System.IO.File.WriteAllText(filePath, save);
        Debug.Log($"Saved game in slot {indexOfSave}: {saveInfo}");

        if (allSaves[indexOfSave] != default) allSaves[indexOfSave] = saveInfo;
       
    }

    public static void LoadGame(this MonoBehaviour x, int indexOfSave)
    {
        var filePath = Application.persistentDataPath + $"/SavedGame{indexOfSave}.json";
        var data = JsonUtility.FromJson<SaveInfo>(System.IO.File.ReadAllText(filePath));

        var done = x.AsyncLoader(data.sceneToKeep);
        x.ExecuteAfterTrue(() => done, () =>
        {
            GameObject.FindObjectOfType<ThaniaHealth>().transform.position = data.spawnPosition;
        });   
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

    public string sceneToKeep;
    public Vector2 spawnPosition;

    public SaveInfo(int saveSpot, Vector2 spawnPos,  int health)
    {
        playerHealth = health;
        spawnPosition = spawnPos;

        switch (saveSpot)
        {
            case 0:
                sceneToKeep = SceneManager.GetSceneByName("Level 1").name; break;
            case 1:
                sceneToKeep = SceneManager.GetSceneByName("Level 1").name; break;
            case 2:
                sceneToKeep = SceneManager.GetSceneByName("Level 2").name; break;
            case 3:
                sceneToKeep = SceneManager.GetSceneByName("Level 2").name; break;
            case 4:
                sceneToKeep = SceneManager.GetSceneByName("Level 3").name; break;
            case 5:
                sceneToKeep = SceneManager.GetSceneByName("Level 3").name; break;
            case 6:
                sceneToKeep = SceneManager.GetSceneByName("Level 4").name; break;
            case 7:
                sceneToKeep = SceneManager.GetSceneByName("Level 4").name; break;
            default: break;

        }
    }
}
