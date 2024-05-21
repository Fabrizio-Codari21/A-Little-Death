using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIUpdater : MonoBehaviour
{
    public GameObject healthText;
    public GameObject thania;
    ThaniaHealth health;
    TextMeshProUGUI TMPHealth;

    void Start()
    {
        TMPHealth = healthText.GetComponent<TextMeshProUGUI>();
        health = thania.GetComponent<ThaniaHealth>();
    }

    void Update()
    {
        TMPHealth.text = health.currentHealth.ToString();
    }
}
