using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveManager
{
    public static SaveInfo[] allSaves = new SaveInfo[3];

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

        if(data.sceneToKeep == string.Empty) { Debug.Log("There is no data"); return; }

        var done = x.AsyncLoader(data.sceneToKeep);
        x.ExecuteAfterTrue(done, () =>
        {
            GameObject.FindObjectOfType<ThaniaHealth>().transform.position = data.spawnPosition;
            Debug.Log("ahora arranco el nivel");
        });   
    }

    public static void GetSavedGames()
    {
        for (int i = 0; i < allSaves.Length; i++)
        {
            var save = JsonUtility.FromJson<SaveInfo>
                       (System.IO.File.ReadAllText
                       (Application.persistentDataPath + $"/SavedGame{i + 1}.json"));

            if (save != null) allSaves[i] = save; 
            else System.IO.File.WriteAllText
                 (Application.persistentDataPath + $"/SavedGame{i + 1}.json", 
                 default);
        }
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

    //public static int GetSavedGameIndex(SaveInfo savedInfo)
    //{
    //    foreach (var save in allSaves)
    //    {
    //        if (save == savedInfo)
    //        {
    //            return allSaves. + 1;
    //        }
    //    }

    //    Debug.Log("The requested save index does not exist");
    //    return -1;
    //}
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
                sceneToKeep = SceneManager.GetSceneByBuildIndex(1).name; Debug.Log("0"); break;
            case 1:
                sceneToKeep = SceneManager.GetSceneByBuildIndex(1).name; Debug.Log("1"); break;
            case 2:
                sceneToKeep = SceneManager.GetSceneByBuildIndex(2).name; Debug.Log("2"); break;
            case 3:
                sceneToKeep = SceneManager.GetSceneByBuildIndex(2).name; Debug.Log("3"); break;
            case 4:
                sceneToKeep = SceneManager.GetSceneByBuildIndex(3).name; Debug.Log("4"); break;
            case 5:
                sceneToKeep = SceneManager.GetSceneByBuildIndex(3).name; Debug.Log("5"); break;
            case 6:
                sceneToKeep = SceneManager.GetSceneByBuildIndex(4).name; Debug.Log("6"); break;
            case 7:
                sceneToKeep = SceneManager.GetSceneByBuildIndex(4).name; Debug.Log("7"); break;
            default: break;

        }
    }
}
