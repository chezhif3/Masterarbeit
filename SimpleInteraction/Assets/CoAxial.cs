using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoAxial : MonoBehaviour
{
    bool captured = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    void Update()
    {
        int layerMask = (1 << 0)|(1 << 3);

        RaycastHit hit;
        GameObject gameObject;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up),out hit, Mathf.Infinity,layerMask)&&(!captured))
        {
            Debug.Log("Did Hit");
            gameObject = hit.transform.gameObject;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.transform.position = transform.position + Vector3.Dot((gameObject.transform.position- transform.position), transform.TransformDirection(Vector3.up)) * Vector3.Normalize(transform.TransformDirection(Vector3.up));
            captured = true;

        }
        //else
        //{
        //    Debug.Log("Did not Hit");
        //}

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 10, Color.yellow);
    }
}
