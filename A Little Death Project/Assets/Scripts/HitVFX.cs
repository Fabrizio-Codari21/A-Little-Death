using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitVFX : MonoBehaviour
{
    public void Disable()
    {
        Destroy(gameObject, 0.02f);
    }
}
