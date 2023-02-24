using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoAxial : MonoBehaviour
{
    // bool captured = false;
    // Start is called before the first frame update
    //  public Transform nextObject;
    public float PostionTolerance;
    public float AngleTolerance;
    //   public Material _material;
    public Vector3 RayCastDirection;
    public bool RayCastManuel=false;
    public Transform startPosition;
    public Transform endPosition;
    public float RayCastDistance = Mathf.Infinity;
    private bool isDone = false;

    void Start()
    {
        if (!RayCastManuel)
        {
            RayCastDirection = endPosition.position - startPosition.position;
        }
        Debug.Log(transform.parent.Find("Mesh").GetChild(0).localPosition);
        Debug.Log(transform.parent.Find("Mesh").GetChild(1).localPosition);
    }

    // Update is called once per frame

    void Update()
    {
        int layerMask = (1 << 0) | (1 << 3);

        RaycastHit hit;
        GameObject currObject;
        GameObject parentObject;
        GameObject nextObject;
        //float referenceAngle;
        //float isAngle;

        if (!isDone)
        {
            if (Physics.Raycast(transform.position, RayCastDirection, out hit, RayCastDistance, layerMask))
            {
                Debug.Log("Did Hit");
                currObject = hit.transform.gameObject;
                parentObject = currObject.transform.parent.gameObject;
                Debug.Log(currObject.name);
                Debug.Log(parentObject.name);
                if (parentObject.GetComponent<ComponentState>().assemblePhase == ComponentState.AssemblePhase.Pairing)
                {
                    nextObject = parentObject.transform.Find("LinearDrive").gameObject;
                    nextObject.transform.position = currObject.transform.position;
                    // nextObject.transform.Find("Handle").position = currObject.transform.position;
                    // nextObject.transform.rotation = gameObject.transform.rotation;
                    Destroy(currObject);
                    nextObject.SetActive(true);
                    //gameObject.GetComponent<Rigidbody>().useGravity = false;
                    //gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    //gameObject.transform.position = transform.position + Vector3.Dot((gameObject.transform.position- transform.position), transform.TransformDirection(Vector3.up)) * Vector3.Normalize(transform.TransformDirection(Vector3.up));
                    parentObject.GetComponent<ComponentState>().assemblePhase = ComponentState.AssemblePhase.Insertion;
                }
                if (parentObject.GetComponent<ComponentState>().assemblePhase == ComponentState.AssemblePhase.Insertion)
                {
                    Debug.Log("Inserted");
                    currObject = parentObject.transform.Find("LinearDrive").gameObject;
                    Debug.Log(currObject.transform.localEulerAngles.x );
                    Debug.Log(transform.eulerAngles.x);
                    Debug.Log((currObject.transform.eulerAngles.x - transform.eulerAngles.x));
                    //referenceAngle = currObject.transform.rotation.ToAngleAxie(out isAngle, out );
                    if ((currObject.transform.position - transform.position).magnitude <= PostionTolerance &&( ((currObject.transform.eulerAngles.x- transform.eulerAngles.x)%90 <= AngleTolerance )| (currObject.transform.eulerAngles.x - transform.eulerAngles.x)<= AngleTolerance ))
                    {
                        Destroy(currObject);
                        transform.Find("Reference").gameObject.SetActive(false);
                        transform.Find("Solution").gameObject.SetActive(true);
                        isDone = true;
                    }
                }
            }
            //else
            //{
            //    Debug.Log("Did not Hit");
            //}

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 10, Color.yellow);
        }
    }
}
