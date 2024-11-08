using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Cadaver : MonoBehaviour
{
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
