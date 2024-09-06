using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    Image timer;
    float maxTime = 10;
    float timeLeft;
    public bool timerActive;

    void Start()
    {
        timer = GetComponent<Image>();
        timeLeft = maxTime;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (timerActive == true)
        {
            if(timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                timer.fillAmount = timeLeft/maxTime;
            }
            else
            {
                timeLeft = maxTime;
                timer.gameObject.SetActive(false);
            }
        }
    }
}
