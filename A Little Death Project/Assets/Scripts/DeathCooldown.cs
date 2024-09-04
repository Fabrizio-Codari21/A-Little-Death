using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCooldown : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Tutorial");
    }
}
