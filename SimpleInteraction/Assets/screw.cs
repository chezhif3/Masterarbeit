using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screw : MonoBehaviour
{
    public Material material;
    public bool ifReady = false;
    public bool ifDone = false;
    private bool open = true;

    private void OnTriggerEnter(Collider other)
    {
        if(open)
        {
            if (other.tag == "screw")
            {
                Debug.Log("scews");
                Destroy(other.gameObject);
                GetComponent<Renderer>().material = material;
                ifReady = true;
                open = false;
            }
        }
    }
}
