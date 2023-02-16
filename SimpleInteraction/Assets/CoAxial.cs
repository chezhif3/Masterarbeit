using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoAxial : MonoBehaviour
{
    // bool captured = false;
    // Start is called before the first frame update
  //  public Transform nextObject;
    void Start()
    {

    }

    // Update is called once per frame

    void Update()
    {
        int layerMask = (1 << 0)|(1 << 3);

        RaycastHit hit;
        GameObject gameObject;
        GameObject parentObject;
        GameObject nextObject;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up),out hit, Mathf.Infinity,layerMask))
        {
            Debug.Log("Did Hit");
            gameObject = hit.transform.gameObject;
            parentObject = gameObject.transform.parent.gameObject;
            Debug.Log(gameObject.name);
            Debug.Log(parentObject.name);
            if (parentObject.GetComponent<ComponentState>().assemblePhase == ComponentState.AssemblePhase.Pairing)
            {
                nextObject = parentObject.transform.Find("LinearDrive").gameObject;
                nextObject.transform.position = gameObject.transform.position;
                nextObject.transform.Find("Handle").position = gameObject.transform.position;
                // nextObject.transform.rotation = gameObject.transform.rotation;
                Destroy(gameObject);
                nextObject.SetActive(true);
                //gameObject.GetComponent<Rigidbody>().useGravity = false;
                //gameObject.GetComponent<Rigidbody>().isKinematic = true;
                //gameObject.transform.position = transform.position + Vector3.Dot((gameObject.transform.position- transform.position), transform.TransformDirection(Vector3.up)) * Vector3.Normalize(transform.TransformDirection(Vector3.up));
                parentObject.GetComponent<ComponentState>().assemblePhase = ComponentState.AssemblePhase.Insertion;
            }
        }
        //else
        //{
        //    Debug.Log("Did not Hit");
        //}

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 10, Color.yellow);
    }
}
