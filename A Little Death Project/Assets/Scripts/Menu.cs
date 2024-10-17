using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject fadeEfect;
    public Opciones settings;

    private void Start()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (this.Inputs(MyInputs.Secret1))
        {
            StartCoroutine(waitForTransition("Level 2"));
        }
    }

    public void Play()
    {
        StartCoroutine(waitForTransition("Level 1"));
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
        StartCoroutine(waitForTransition(SceneManager.GetActiveScene().name));
    }

    public void Quit()
    {
        Application.Quit();
    }

    public IEnumerator waitForTransition(string name)
    {
        fadeEfect.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        this.AsyncLoader(name);
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
        this.AsyncLoader("MainMenu");

    }
}
