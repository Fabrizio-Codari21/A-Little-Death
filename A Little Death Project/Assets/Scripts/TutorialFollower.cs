using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFollower : MonoBehaviour
{
    public GameObject objective;
    [SerializeField] Vector3 offset;
   
    void Update()
    {
        transform.position = objective.transform.position + offset;
    }
}
