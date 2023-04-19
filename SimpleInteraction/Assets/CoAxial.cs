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


    public bool isDone = false;

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

    // private int temp=0;

    void Update()
    {
        int layerMask1 =  1 << 6;
        int layerMask2 = 1 << 6;

        RaycastHit hit;
        GameObject currObject;
        GameObject parentObject;
        Vector3 currPosition;
        

        GameObject capturedObject;



        if (!isDone)
        {
            // isDone = true;
            // temp = temp+1;
            // Debug.Log(temp);
            // Debug.Log(isDone);
            // Debug.Log(startPosition.position);
            // Debug.DrawRay(startPosition.position, RayCastDirection, Color.yellow);
            if (Physics.Raycast(startPosition.position, RayCastDirection, out hit, RayCastDistance, layerMask1)&& Physics.Raycast(startPosition.position, RayCastDirection, out hit, RayCastDistance, layerMask2))
            //if (Physics.Raycast(startPosition.position, RayCastDirection,out hit, RayCastDistance,layerMask2 ) )
            {
                Debug.Log("Did Hit");
                Debug.Log(hit.transform.gameObject.name);
                currObject = hit.transform.parent.gameObject;
                //parentObject = transform.parent.parent.gameObject;
                // Debug.Log( hit.transform.parent.gameObject.name);
                Debug.Log(transform.parent.parent.parent.parent.gameObject.name);
                transform.parent.parent.parent.parent.gameObject.GetComponent<ComponentState>().capturedObject = hit.transform.parent.gameObject;
                if (hit.transform.parent.gameObject.GetComponent<ComponentState>().assemblePhase == ComponentState.AssemblePhase.Pairing)
                {
                    // currPosition = currObject.transform.position;
                    // Debug.Log(startPosition.position);
                    //Debug.Log("Destroy:" + parentObject.name);
                    //Destroy(parentObject);
                    //currObject.transform.Find("Collider")
                    //currObject.transform.parent = transform;
                    Debug.Log("ChangingPhase");
                    currObject.gameObject.GetComponent<ComponentState>().PhaseChange(ComponentState.AssemblePhase.Insertion);
                    currObject.transform.Find("LinearDrive(Clone)").Find("Start").position = transform.Find("Start").position;
                    currObject.transform.Find("LinearDrive(Clone)").Find("Start").SetParent(transform.Find("Start"));
                    currObject.transform.Find("LinearDrive(Clone)").Find("End").position = transform.Find("End").position;
                    currObject.transform.Find("LinearDrive(Clone)").Find("End").SetParent(transform.Find("End"));
                    //currObject.GetComponent<ComponentState>().SetStartEnd(transform.Find("Start"), transform.Find("End"));
                    // nextObject = prefab;
                    // nextObject.GetComponent<ComponentState>().SetStartEnd(startPosition, endPosition);
                    // Debug.Log(nextObject.GetComponent<ComponentState>().assemblePhase);
                    // Debug.Log(startPosition.position);
                    // GameObject refOb = transform.Find("Reference").gameObject;
                    // Debug.Log(nextObject.GetComponent<ComponentState>().assemblePhase);
                    // Debug.Log(startPosition.position);
                    // Debug.Log(prefab.GetComponent<ComponentState>().assemblePhase);
                    // Debug.Log("spwan:" + nextObject.name);
                    // Instantiate(nextObject , currPosition, refOb.transform.rotation, transform);
                    // Debug.Log(startPosition.position);
                }
                else if (hit.transform.parent.gameObject.GetComponent<ComponentState>().assemblePhase == ComponentState.AssemblePhase.Insertion)
                {
                    // Debug.Log("Inserted");
                    // Debug.Log(startPosition.position);
                    // currObject =parentObject.transform.Find("LinearDrive").gameObject;
                    // nextObject = prefab;
                    // if ((hit.transform.parent.gameObject.name.transform.position - startPosition.position).magnitude > 0.4)
                    // {
                    //     currPosition = currObject.transform.position;
                    //    // Destroy(parentObject);
                    //     nextObject = prefab;
                    //     nextObject.GetComponent<ComponentState>().assemblePhase = ComponentState.AssemblePhase.Pairing;
                    //     Instantiate(nextObject, currPosition, Quaternion.identity);
                    // }
                    // if ((currObject.transform.position - transform.position).magnitude <= PostionTolerance &&( ((currObject.transform.eulerAngles.x- transform.eulerAngles.x)%90 <= AngleTolerance )| (currObject.transform.eulerAngles.x - transform.eulerAngles.x)<= AngleTolerance ))
                    // {
                    //    // Destroy(currObject);
                    //     GameObject refOb = transform.Find("Reference").gameObject;
                    //     Instantiate(nextObject, refOb.transform.position, refOb.transform.rotation,transform);
                    //     transform.Find("Reference").gameObject.SetActive(false);
                    //    // transform.Find("Solution").gameObject.SetActive(true);
                    //     isDone = true;
                    // }
                }
            }
            else
            {
                Debug.Log("Did not Hit");
            }

            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 10, Color.yellow);
        }
    }
}
