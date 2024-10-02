using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    Image timer;
    Image otherTimer;
    [HideInInspector] public float maxTime = 10;
    [HideInInspector] public float timeLeft;
    public bool timerActive;
    public GameObject UI;

    void Start()
    {
        timer = GetComponent<Image>();
        timeLeft = maxTime;
        UI.gameObject.SetActive(false);
    }

    //private void OnEnable()
    //{
    //    timeLeft = maxTime;
    //}

    void Update()
    {
        if (timerActive == true)
        {
            if(timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                timer.fillAmount = timeLeft/maxTime;
                otherTimer.color = new Color(timer.color.r, timer.color.g, timer.color.b, timeLeft / maxTime);

            }
            else
            {
                timeLeft = maxTime;
                UI.gameObject.SetActive(false);
            }
        }
    }
}
