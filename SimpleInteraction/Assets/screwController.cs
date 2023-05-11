using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screwController : MonoBehaviour
{
    public GameObject RecordObj;
    public GameObject WorkStation;
    private bool ifCheck = true;
    public int pre = 0;

    void Awake()
    {
        RecordObj = GameObject.Find("Profiling");
        WorkStation = GameObject.Find("Cube");
    }

    void FixedUpdate()
    {
        if (ifCheck)
        {
           int i = 0;
            foreach (Transform child in transform)
            {
                if (child.GetComponent<screw>().ifDone)
                {
                    i++;
                }
            }
            if (i == 6&&pre==5)
            {
                WorkStation.SendMessage("NextStep");
                object[] parameters = new object[] { 2, 3, Time.time };
                RecordObj.SendMessage("AttemptSuccess", parameters);
                RecordObj.SendMessage("printRecord");
                ifCheck = false;
            }
            pre = i;

        }
    }

}
