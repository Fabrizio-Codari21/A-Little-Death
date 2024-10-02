using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class COCONUT_REFERENCE : MonoBehaviour
{
    public Image orange;

    private float r;
    private float g;
    private float b;
    private float a;

    public float possesionTime = 12f;

    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        r = orange.color.r; 
        g = orange.color.g; 
        b = orange.color.b; 
        a = orange.color.a;

        orange.color = new Color(r, g, b, 1);

        isActive = true;
    }

    private void FixedUpdate()
    {
        print("hola");

        if (isActive)
        orange.color = new Color(r, g, b, orange.color.a - (1 / 50f / possesionTime)); //THIS IS THE REAL SHIT. COCONUT JPG REFERENCE tf2

        if (orange.color.a <= 0)
        {
            isActive = false;
        }

    }

}
