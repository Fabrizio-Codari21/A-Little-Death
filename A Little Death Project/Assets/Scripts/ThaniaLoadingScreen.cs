using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThaniaLoadingScreen : MonoBehaviour
{
    [SerializeField] Image screen;
    [SerializeField] RawImage thania;

    private void Update()
    {
        thania.color = new Color(Color.white.r, Color.white.g, Color.white.b, screen.color.a /*- 0.5f*/);
    }
}
