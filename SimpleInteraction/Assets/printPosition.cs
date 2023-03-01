using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class printPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(name);
        Debug.Log(transform.position);
        Debug.Log(transform.localPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
