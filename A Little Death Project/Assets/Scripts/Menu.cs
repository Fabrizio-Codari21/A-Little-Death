using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject fadeEfect;

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void Play()
    {
        StartCoroutine(waitForTransition("USP TEST"));
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
        SceneManager.LoadScene(name);
    }

    IEnumerator waitForTransitionRestart()
    {
        fadeEfect.SetActive(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    
    IEnumerator waitForTransitionMenu()
    {
        fadeEfect.SetActive(true);
        Time.timeScale = 1;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenu");
    }
}
