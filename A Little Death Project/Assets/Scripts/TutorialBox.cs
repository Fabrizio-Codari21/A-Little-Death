using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialBox : MonoBehaviour
{
    [SerializeField] Vector2 target;
    Vector2 startPos;
    [SerializeField] float speed;
    public bool moving;
    public bool movingBack;
    public string textToPrint;
    public TextMeshProUGUI TMPTutorial;

    private void Start()
    {
        startPos = transform.position;    
    }

    private void OnEnable()
    {
        movingBack = false;
        moving = true;
    }

    void Update()
    {
        if(moving)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        else if(movingBack) 
        {
            transform.position = Vector2.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
        }

        if (transform.position.y >= target.y)
        {
             moving = false;
        }
        else if(transform.position.y <= startPos.y + 100 && moving == false)
        {
            movingBack = false;
            gameObject.SetActive(false);
        }

        TMPTutorial.text = textToPrint;
    }
}
