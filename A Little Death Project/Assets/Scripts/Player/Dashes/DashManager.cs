using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashManager : MonoBehaviour
{
    public DashList[] dashTypes;
    public Dictionary<string, DashList> dashType = new Dictionary<string, DashList>();
    public string dashID;

    private void Start()
    {
        foreach (DashList i in dashTypes)
        {
            dashType.Add(i.classType, i);
            Debug.Log(i.classType);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (dashType.ContainsKey(dashID))
            {
                dashType[dashID].Dash();
            }
            else
            {
                dashType["base"].Dash();
            }
        }
    }
}
