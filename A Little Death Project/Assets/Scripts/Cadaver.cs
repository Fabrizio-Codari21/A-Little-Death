using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cadaver : MonoBehaviour
{
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
