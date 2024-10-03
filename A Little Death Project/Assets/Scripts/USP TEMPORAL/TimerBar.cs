using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    Image timer;
    public Image border;
    [HideInInspector] public float maxTime = 10;
    [HideInInspector] public float timeLeft;
    public bool timerActive;
    public GameObject UI;
    public float borderEffectSpeed;

    Color _invisible;
    Color _original;

    void Start()
    {
        _invisible = new Color(1, 1, 1, 0);
        _original = border.color;
        
        timer = GetComponent<Image>();
        timeLeft = maxTime;
        UI.gameObject.SetActive(false);
    }

    //private void OnEnable()
    //{
    //    timeLeft = maxTime;
    //}

    bool _fadingIn;
    void Update()
    {
        if (timerActive == true)
        {
            if(timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                timer.fillAmount = timeLeft/maxTime;
                BorderEffect(borderEffectSpeed * timeLeft);
            }
            else
            {
                timeLeft = maxTime;
                UI.gameObject.SetActive(false);
            }
        }
    }

    void BorderEffect(float speed)
    {
        if (_fadingIn) border.color = Color.Lerp(border.color, _original, ((timeLeft / speed) / maxTime));
        else border.color = Color.Lerp(border.color, _invisible, ((timeLeft / speed) / maxTime));

        print(border.color.a);

        if (border.color.a <= 0.25f) _fadingIn = true;

        else if (border.color.a >= 0.75f) _fadingIn = false;
    }

}
