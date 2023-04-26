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

    public GameObject RecordObj;
    public GameObject WorkStation;


    public bool isDone = false;

    public Quaternion temp;

    void Start()
    {
        if (!RayCastManuel)
        {
            if(!inverseRaydir)
            { RayCastDirection = endPosition.position - startPosition.position; }
            else
            { RayCastDirection = startPosition.position - endPosition.position; }
        }
        RecordObj = GameObject.Find("Profiling");
        WorkStation = GameObject.Find("Cube");
    }

    // Update is called once per frame

    // private int temp=0;

    void Update()
    {
        int layerMask1 =  1 << 6;
        int layerMask2 = 1 << 6;

        RaycastHit hit;
        GameObject currObject;
        GameObject parentObject = transform.parent.parent.parent.parent.gameObject;
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
                Debug.Log("Did Hit"+hit.transform.gameObject.name);
                currObject = hit.transform.parent.gameObject;
                //parentObject = transform.parent.parent.gameObject;
                // Debug.Log( hit.transform.parent.gameObject.name);
                Debug.Log(transform.parent.parent.parent.parent.gameObject.name);
                transform.parent.parent.parent.parent.gameObject.GetComponent<ComponentState>().capturedObject = hit.transform.parent.gameObject;
                if (hit.transform.parent.gameObject.GetComponent<ComponentState>().assemblePhase == ComponentState.AssemblePhase.PairingB)
                {
                    // currPosition = currObject.transform.position;
                    // Debug.Log(startPosition.position);
                    //Debug.Log("Destroy:" + parentObject.name);
                    //Destroy(parentObject);
                    //currObject.transform.Find("Collider")
                    //currObject.transform.parent = transform;
                    Debug.Log("ChangingPhase");
                    //temp = hit.transform.position;
                    hit.transform.position = startPosition.position + ((endPosition.position - startPosition.position).normalized) *((hit.transform.position - startPosition.position).magnitude * Mathf.Cos(Mathf.PI*Vector3.Angle(endPosition.position - startPosition.position, hit.transform.position - startPosition.position)/180));
                    MeshFilter[] Objs = currObject.transform.GetComponentsInChildren<MeshFilter>();
 
                   // hit.transform.rotation = transform.rotation * Objs[0].gameObject.transform.parent.localRotation;
                    foreach (MeshFilter obj in Objs)
                    {
                        
                        if(obj.name == (currObject.gameObject.GetComponent<ComponentState>().Index.ToString()+"Mesh"))
                        {
                            hit.transform.rotation = transform.rotation*Quaternion.Inverse(obj.transform.localRotation);
                            temp = obj.transform.localRotation;
                            Debug.Log("scren;" + obj.name);
                            Debug.Log("Rerotate");
                        }
                    }
                    //Debug.Log("11: " + temp.x);
                    //Debug.Log("start: " + startPosition.position+"end: " + endPosition.position + "temp: "+temp+"Vector: "+ (endPosition.position - startPosition.position).normalized);
                    //Debug.Log("1: " + (temp - startPosition.position).magnitude);
                    //Debug.Log("2: " + (endPosition.position - startPosition.position));
                    //Debug.Log("Angle: " + Vector3.Angle(temp - startPosition.position, endPosition.position - startPosition.position));
                    //Debug.Log("var1: " + Vector3.Dot(temp - startPosition.position, endPosition.position - startPosition.position) + " var2: " + (temp - startPosition.position).magnitude  * Mathf.Cos(Mathf.PI * Vector3.Angle(endPosition.position - startPosition.position, temp - startPosition.position) / 180));
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
                     Debug.Log("Inserted");
                    // Debug.Log(startPosition.position);
                    // currObject =parentObject.transform.Find("LinearDrive").gameObject;
                    // nextObject = prefab;
                    if ((hit.transform.position - startPosition.position).magnitude > ((endPosition.position - startPosition.position).magnitude-0.01))
                    {
                        object[] parameters = new object[] { currObject.GetComponent<ComponentState>().Index, 3, Time.time };
                        RecordObj.SendMessage("AttemptMistake", parameters);
                        RecordObj.SendMessage("printRecord");
                        Destroy(currObject);
                    }
                    else if ((hit.transform.position - startPosition.position).magnitude <= PostionTolerance )
                    //&&( ((currObject.transform.eulerAngles.x- transform.eulerAngles.x)%90 <= AngleTolerance )| (currObject.transform.eulerAngles.x - transform.eulerAngles.x)<= AngleTolerance ))
                    {
                        // Destroy(currObject);
                        // GameObject refOb = transform.Find("Reference").gameObject;
                        // Instantiate(nextObject, refOb.transform.position, refOb.transform.rotation,transform);
                        // transform.Find("Reference").gameObject.SetActive(false);
                        // transform.Find("Solution").gameObject.SetActive(true);
                        // transform.gameObject.SetActive(false);
                        //transform.parent.parent.parent.parent.gameObject.GetComponent<ComponentState>().AddCollider();
                        WorkStation.SendMessage("NextStep");
                        currObject.SetActive(false);
                        //string _name = name;
                        currObject.transform.position = parentObject.transform.Find("Sockets").Find(name).Find("Reference").position;
                        currObject.transform.rotation =parentObject.transform.Find("Sockets").Find(name).Find("Reference").rotation;
                        if(name.Contains(currObject.GetComponent<ComponentState>().Index.ToString()))
                        {
                            hit.transform.gameObject.SetActive(false);
                            parentObject.GetComponent<ComponentState>().CloseSocket(currObject.GetComponent<ComponentState>().Index);

                        }

                        MeshFilter[] Objs = currObject.transform.GetComponentsInChildren<MeshFilter>();
                        MeshFilter[] Objss = transform.parent.GetComponentsInChildren<MeshFilter>();

                        // hit.transform.rotation = transform.rotation * Objs[0].gameObject.transform.parent.localRotation;
                        foreach (MeshFilter obj in Objs)
                        {

                            if (obj.name == (currObject.gameObject.GetComponent<ComponentState>().Index.ToString() + "Mesh"))
                            {
                                hit.transform.rotation = transform.rotation * Quaternion.Inverse(obj.transform.localRotation);
                                //temp = Quaternion.Inverse(obj.transform.localRotation);
                            }
                        }
                        foreach (MeshFilter obj in Objss)
                        {

                            if (obj.name == (parentObject.GetComponent<ComponentState>().Index.ToString() + "Mesh"))
                            {
                                Debug.Log(obj.name);
                                 hit.transform.rotation = transform.rotation * Quaternion.Inverse(obj.transform.localRotation);
                                //temp = obj.transform.localRotation*temp;
                            }
                        }
                        foreach (Transform child in currObject.transform.Find("Colliders"))
                        {
                            parentObject.GetComponent<ComponentState>().AddCollider(child.gameObject);
                        }
                        foreach(Transform child in currObject.transform.Find("Sockts"))
                        {
                            parentObject.GetComponent<ComponentState>().AddSockets(child.gameObject);
                        }
                        Destroy(currObject);
                        object[] parameters = new object[] { currObject.GetComponent<ComponentState>().Index, 2, Time.time };
                        RecordObj.SendMessage("AttemptSuccess", parameters);
                        RecordObj.SendMessage("printRecord");
                        isDone = true;
                      
                    }
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
