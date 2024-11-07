using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject fadeEfect;
    public Opciones settings;
    //[SerializeField] int levelToLoad;

    private void Start()
    {
        SaveManager.GetSavedGames();
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (this.Inputs(MyInputs.Secret1))
        {
            var next = SceneManager.GetActiveScene().buildIndex + 1;
            if (next is 1 or 2 or 3 or 4)
            {
                StartCoroutine(waitForTransition("Level " + next.ToString()));
            }
        }
    }

    public void Play(int levelToLoad)
    {
        if (levelToLoad is 1 or 2 or 3 or 4)
        {
            for (int i = 0; i < SaveManager.allSaves.Length; i++)
            {
                if (SaveManager.allSaves[i] == default) { SaveManager.currentSave = i; break; }
            }
            StartCoroutine(waitForTransition("Level " + levelToLoad.ToString()));

        }
    }    
    
    public void Restart()
    {
        StartCoroutine(waitForTransitionRestart());
    }   
    
    public void MainMenu()
    {
        StartCoroutine(waitForTransitionMenu());
    }
    
    public void Death()
    {
        StartCoroutine(waitForTransitionRestart());
    }

    public void Quit()
    {
        Application.Quit();
    }

    public IEnumerator waitForTransition(string name)
    {
        fadeEfect.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Checkpoints.active = false;
        Checkpoints.checkPoint = default;
        this.AsyncLoader(name);

        var save = (SceneManager.GetActiveScene().buildIndex * 2) - 2;

        var saveInfo = new SaveInfo(save,
                                    Checkpoints.savedPos,
                                    5);

        if (saveInfo.sceneToKeep == string.Empty) { print("hubo un problema"); }

        this.WaitAndThen(timeToWait: 1f, () =>
        {
            this.SaveGame(saveInfo, SaveManager.currentSave);
        },
        cancelCondition: () => false);
        
    }

    IEnumerator waitForTransitionRestart()
    {
        fadeEfect.SetActive(true);
        yield return new WaitForSeconds(1);
        //SceneManager.LoadScene();
        this.AsyncLoader(SceneManager.GetActiveScene().name);
    }
    
    IEnumerator waitForTransitionMenu()
    {
        fadeEfect.SetActive(true);
        Time.timeScale = 1;
        yield return new WaitForSeconds(1);
        Checkpoints.active = false;
        Checkpoints.checkPoint = default;
        this.AsyncLoader("MainMenu");
    }

    public TextMeshProUGUI saveWarningText;
    public void LoadGameFromMenu(int x)
    {
        if (SaveManager.allSaves[x - 1] == default)
        {
            if(saveWarningText.gameObject.activeInHierarchy) saveWarningText.gameObject.SetActive(false);

            saveWarningText.gameObject.SetActive(true);

            this.WaitAndThen(timeToWait: 2f, () =>
            {
                saveWarningText.gameObject.SetActive(false);
            },
            cancelCondition: () => !saveWarningText.gameObject.activeInHierarchy);

            return; 
        }

        fadeEfect.SetActive(true);
        this.WaitAndThen(timeToWait: 1.5f, () =>
        {
            this.LoadGame(x);
        },
        cancelCondition: () => false);
    }


    public void DeleteGameFromMenu(int x)
    {
        SaveManager.DeleteGame(x);
    }
}
