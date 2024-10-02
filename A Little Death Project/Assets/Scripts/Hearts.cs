using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    public Image[] hearts;

    public Sprite fullHeart;
    public Sprite emptyHeart;

    public GameObject thania;
    ThaniaHealth thaniaHealth;
    [SerializeField] ParticleSystem particleSystems;

    private void Start()
    {
        thaniaHealth = thania.GetComponent<ThaniaHealth>();
    }

    private void Update()
    {
        health = thaniaHealth.currentHealth;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                if(hearts[i].gameObject.activeInHierarchy)
                {
                    //Instantiate(particleSystems, hearts[i].transform.position, Quaternion.Euler(270, 0, 0));
                    particleSystems.transform.position = hearts[i].transform.position;
                    particleSystems.Play();
                }
                hearts[i].gameObject.SetActive(false); 
                
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
