using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreepersMenu : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Loop());
    }

    IEnumerator Loop()
    {
        int time = Random.Range(0, 4);
        yield return new WaitForSeconds(time);
        transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(Loop());
    }
}
