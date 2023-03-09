using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class ShowCenter : MonoBehaviour
{
    public Vector3 center;

    // Update is called once per frame
    void Update()
    {
        center = GetComponent<Renderer>().bounds.center;
    }
}
