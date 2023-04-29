using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Record : MonoBehaviour
{
    public List<object[]> arrayMistake;
    public List<object[]> arraySuccess;

    public string fileName1 = "Mistake.txt"; // 
    public string fileName2 = "Success.txt"; // 

    public string filePath1 = "M:/privat/test/Update/SimpleInteraction/Assets/Mistake.txt"; // 
    public string filePath2 = "M:/privat/test/Update/SimpleInteraction/Assets/Success.txt"; // 

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

    void OnApplicationQuit()
    {
        printRecord();

        //string filePath = Path.Combine(Application.persistentDataPath, fileName); // 
       //string mistakeString = string.Join(",", arrayMistake); // 
       // string successString = string.Join(",", arraySuccess);

        //for (int i = 0; i < arrayMistake.Count; i++)
        //{
        //    string rowMistake = "RowMistake " + i + ": " + string.Join(",", arrayMistake[i]);
        //    File.WriteAllText(filePath1, rowMistake);
        //    Debug.Log(filePath1);
        //}
        //for (int i = 0; i < arraySuccess.Count; i++)
        //{
        //    string rowSuccess = "RowSuccess " + i + ": " + string.Join(",", arraySuccess[i]);
        //    File.WriteAllText(filePath2, rowSuccess);
        //    Debug.Log(filePath2);
        //}

        //File.WriteAllText(filePath1, mistakeString); // 
        //File.WriteAllText(filePath2,successString); // 

        using (StreamWriter writer = new StreamWriter("M:/privat/test/Update/SimpleInteraction/Assets/Mistake.txt"))
        {
            for (int i = 0; i < arrayMistake.Count; i++)
            {
                string rowMistake = "RowMistake " + i + ": " + string.Join(";", arrayMistake[i]);
                writer.WriteLine(rowMistake);
                Debug.Log("patha: "+filePath1);
            }
        }
        using (StreamWriter writer = new StreamWriter("M:/privat/test/Update/SimpleInteraction/Assets/Success.txt"))
        {
            for (int i = 0; i < arraySuccess.Count; i++)
            {
                string rowSuccess = "RowSuccess " + i + ": " + string.Join(";", arraySuccess[i]);
                writer.WriteLine(rowSuccess);
                Debug.Log("pathb: "+filePath2);
            }
        }
        Debug.Log("-----ending-----");
    }

}
