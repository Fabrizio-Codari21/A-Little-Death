using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    Image timer;
    public Image border;
    public Image border2;
    public float maxTime = 10;
    public float timeLeft;
    public bool timerActive;
    public GameObject UI;
    public float borderEffectSpeed;
    public float slowDownTime;
    public SpriteRenderer dissolve;

    Color _invisible;
    Color _original;

    void Start()
    {
        _invisible = new Color(1, 1, 1, 0);
        _original = border.color;
        slowdown = borderEffectSpeed / (maxTime / 2);


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
            /*if(timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                timer.fillAmount = timeLeft/maxTime;
                var borderSpeed = timeLeft > 1 ? (borderEffectSpeed / timeLeft) : borderEffectSpeed;
                BorderEffect(borderSpeed);
            }
            else
            {
                timeLeft = maxTime;
                UI.gameObject.SetActive(false);
                slowdown = borderEffectSpeed / (maxTime / 2);
            }*/

            timeLeft -= Time.deltaTime;
            border.fillAmount = timeLeft / maxTime;
            border2.fillAmount = timeLeft / maxTime;
        }
    }

    float slowdown;
    void BorderEffect(float speed)
    {
        float sp = (speed / slowdown / timeLeft);
           
        if (_fadingIn) border.color = Color.Lerp(border.color, _original, (sp / maxTime));
        else border.color = Color.Lerp(border.color, _invisible, (sp / maxTime));

        if (timeLeft < slowDownTime)
        {
            slowdown += (1 / timeLeft);
            border.color -= new Color(0.004f, 0.002f, 0.001f, timeLeft > 1 ? 0.001f : 0.0075f);
        }

        //print(border.color.a);

        if (border.color.a <= 0.25f && timeLeft > 1f) _fadingIn = true;

        else if (border.color.a >= 0.75f || timeLeft <= 0.5f) _fadingIn = false;
    }

    public void ActivateTimer(bool active)
    {
        timerActive = active;
        timeLeft = maxTime;

        if (dissolve) this.ExecuteUntil(timeLimit: maxTime, () =>
        {
            dissolve.material.SetFloat("Vertical_Dissolve", Mathf.Lerp(1.1f, 0, (timeLeft / maxTime)));
            //Shader.SetGlobalFloat("Vertical_Dissolve", 1.1f - (timeLeft/maxTime));
            Debug.Log(1.1f - (timeLeft / maxTime));

        });

        /*border.color = _original;
        _fadingIn = false;
        slowdown = borderEffectSpeed / (maxTime / 2);*/
    }

}
