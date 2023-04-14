using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record : MonoBehaviour
{
    // Start is called before the first frame update
    List<object[]> array = new List<object[]>();

    void recordAttempt(object[] attempt)
    {
        array.Add(new object[] {attempt[0], attempt[1], attempt[2]});
    }

    // Update is called once per frame

    void printRecord()
    {
        for (int i = 0; i < array.Count; i++)
        {
            string row = "Row " + i + ": " + string.Join(",", array[i]);
            Debug.Log(row);
        }  
    }

}
