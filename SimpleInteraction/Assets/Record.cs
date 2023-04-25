using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record : MonoBehaviour
{
    public List<object[]> arrayMistake;
    public List<object[]> arraySuccess;

    void Start()
    {
        // Start is called before the first frame update
        arrayMistake = new List<object[]>();
        arraySuccess = new List<object[]>();
    }

    void AttemptMistake(object[] attempt)
    {
        arrayMistake.Add(new object[] {attempt[0], attempt[1], attempt[2]});
    }

    void AttemptSuccess(object[] attempt)
    {
        arraySuccess.Add(new object[] { attempt[0], attempt[1], attempt[2]});
    }

    void printRecord()
    {
        for (int i = 0; i < arrayMistake.Count; i++)
        {
            string rowMistake = "RowMistake " + i + ": " + string.Join(",", arrayMistake[i]);
            Debug.Log(rowMistake);
        }
        for (int i = 0; i < arraySuccess.Count; i++)
        {
            string rowSuccess = "RowSuccess " + i + ": " + string.Join(",", arraySuccess[i]);
            Debug.Log(rowSuccess);
        }
    }

}
