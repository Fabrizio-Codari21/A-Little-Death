using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject menu;
    bool paused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            menu.SetActive(true);
            paused = true;
            Time.timeScale = 0;
        } 
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            menu.SetActive(false);
            paused = false;
            Time.timeScale = 1;
        }
    }
}
