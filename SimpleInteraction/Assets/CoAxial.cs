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
    public GameObject prefab;
    public bool inverseRaydir;
    private bool isDone = false;

    void Start()
    {
        if (!RayCastManuel)
        {
            if(!inverseRaydir)
            { RayCastDirection = endPosition.position - startPosition.position; }
            else
            { RayCastDirection = startPosition.position - endPosition.position; }
        }
    }

    // Update is called once per frame

    void Update()
    {
        int layerMask = (1 << 0) | (1 << 3);

        RaycastHit hit;
        GameObject currObject;
        GameObject parentObject;
        Vector3 currPosition;

        GameObject nextObject;


        if (!isDone)
        {
            if (Physics.Raycast(startPosition.position, RayCastDirection, out hit, RayCastDistance, layerMask))
            {
                //Debug.Log("Did Hit");
                currObject = hit.transform.gameObject;
                parentObject = currObject.transform.parent.gameObject;
                //Debug.Log(currObject.name);
               // Debug.Log(parentObject.name);
                if (parentObject.GetComponent<ComponentState>().assemblePhase == ComponentState.AssemblePhase.Pairing)
                {
                    currPosition = currObject.transform.position;
                    Destroy(parentObject);
                    nextObject = prefab;
                    nextObject.GetComponent<ComponentState>().SetStartEnd(startPosition, endPosition);
                    Debug.Log(nextObject.GetComponent<ComponentState>().assemblePhase);
                    nextObject.GetComponent<ComponentState>().assemblePhase = ComponentState.AssemblePhase.Insertion;
                    Debug.Log(nextObject.GetComponent<ComponentState>().assemblePhase);
                    Debug.Log(prefab.GetComponent<ComponentState>().assemblePhase);
                    Instantiate(nextObject , currPosition, Quaternion.identity);
                }
                if (parentObject.GetComponent<ComponentState>().assemblePhase == ComponentState.AssemblePhase.Insertion)
                {
                   // Debug.Log("Inserted");
                    currObject =parentObject.transform.Find("LinearDrive").gameObject;
                    nextObject = prefab;
                    if ((currObject.transform.position - startPosition.position).magnitude > 0.4)
                    {
                        currPosition = currObject.transform.position;
                        Destroy(parentObject);
                        nextObject = prefab;
                        nextObject.GetComponent<ComponentState>().assemblePhase = ComponentState.AssemblePhase.Pairing;
                        Instantiate(nextObject, currPosition, Quaternion.identity);
                    }
                    if ((currObject.transform.position - transform.position).magnitude <= PostionTolerance &&( ((currObject.transform.eulerAngles.x- transform.eulerAngles.x)%90 <= AngleTolerance )| (currObject.transform.eulerAngles.x - transform.eulerAngles.x)<= AngleTolerance ))
                    {
                        Destroy(currObject);
                        GameObject refOb = transform.Find("Reference").gameObject;
                        Instantiate(nextObject, refOb.transform.position, refOb.transform.rotation,transform);
                        transform.Find("Reference").gameObject.SetActive(false);
                       // transform.Find("Solution").gameObject.SetActive(true);
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
