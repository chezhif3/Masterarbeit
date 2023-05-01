using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureOn : MonoBehaviour
{
    public class ListNode
    {
        public int[] value;
        public ListNode next;
    }

    public class StringNode
    {
        public string value;
        public StringNode next;
    }
    public Color regular;
    public Color current;

    private ListNode head;
    private ListNode curr;

    private StringNode heads;
    private StringNode currs;

    public Transform center;
    public int WorkMode=1;

    private int countMaxColliders;
    public int countCurrColliders=0;

    //public GameObject testOb;

    public GameObject RecordObj;

    //public int testNr = 0;

    void Start()
    {
        head = new ListNode();
        head.value = new int[] { 1,2 };
        curr = head;

        ListNode node1 = new ListNode();
        node1.value = new int[] { 3,1 };
        head.next = node1;

        ListNode node2 = new ListNode();
        node2.value = new int[] {5,3};
        node1.next = node2;

        ListNode node3 = new ListNode();
        node3.value = new int[] { 5,4 };
        node2.next = node3;

        heads = new StringNode();
        heads.value = "Place brake disc on left wheel hub";
        currs = heads;

        StringNode nodes1 = new StringNode();
        nodes1.value = "Mount the assembled wheel hub onto the left support arm";
        heads.next = nodes1;

        StringNode nodes2 = new StringNode();
        nodes2.value = "Insert the assembled support arm to the front axle";
        nodes1.next = nodes2;

        StringNode nodes3 = new StringNode();
        nodes3.value = "Place the steering arm on the support arm";
        nodes2.next = nodes3;

        //ListNode node4 = new ListNode();
        //node4.value = new int[] { 5, 4 };
        //node3.next = node4;

        if (WorkMode == 1)
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.name.Contains(curr.value[0].ToString()) || child.gameObject.name.Contains(curr.value[1].ToString()))
                {

                    child.GetComponent<Renderer>().material.color = current;
                }
                else
                {
                    child.GetComponent<Renderer>().material.color = regular;
                }
                if(child.gameObject.name == "Text" )
                {
                    Debug.Log("Find the text");
                    child.GetComponent<TMPro.TextMeshPro>().text = currs.value;
                }
            }
        }
        else
        {
            GameObject[] foundGameObjects = GameObject.FindGameObjectsWithTag("Part");
            foreach (GameObject foundObject in foundGameObjects)
            {
                MeshRenderer[] foundObj = foundObject.transform.Find("Sockets").GetComponentsInChildren<MeshRenderer>();
                foreach(MeshRenderer obj in foundObj)
                {
                    obj.enabled = false;
                }

            }
        }
    }

    void NextStep()
    {
        curr = curr.next;
        currs = currs.next;
        if(curr != null)
        {
            Debug.Log("Next:" + curr.value[0] + curr.value[1]);
            if (WorkMode == 1)
            {
                foreach (Transform child in transform)
                {
                    if (child.gameObject.name.Contains(curr.value[0].ToString()) || child.gameObject.name.Contains(curr.value[1].ToString()))
                    {

                        child.GetComponent<Renderer>().material.color = current;
                    }
                    else
                    {
                        child.GetComponent<Renderer>().material.color = regular;
                    }
                    if (child.gameObject.name == "Text")
                    {
                        child.GetComponent<TMPro.TextMeshPro>().text = currs.value;
                    }
                }
            }
        }
        else
        {
            transform.Find("Text").GetComponent<TMPro.TextMeshPro>().text = "Finished!";
            Debug.Log("Finished!");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().assemblePhase==ComponentState.AssemblePhase.Identification && !other.gameObject.GetComponent<Valve.VR.InteractionSystem.Throwable>().attached)
        {

                int currInt = other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().Index;
                int query = System.Array.IndexOf(curr.value, other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().Index);
                Debug.Log("enterWorkstation:");
                Debug.Log(other.gameObject.transform.parent.name);
                Debug.Log("Index: " + query);
                if (query != -1)
                {
                    if (query == 0)
                    {
                        other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().OpenSocket(curr.value[1]);
                        other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().PhaseChange(ComponentState.AssemblePhase.PairingA);
                        other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().MoveTo(center);
                        countCurrColliders++;
                    }
                    if (query == 1)
                    {
                        other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().PhaseChange(ComponentState.AssemblePhase.PairingB);
                        countCurrColliders++;
                    }
                }
                else
                {
                    other.gameObject.transform.parent.gameObject.SetActive(false);
                    other.gameObject.transform.localPosition = Vector3.zero;
                    other.gameObject.transform.localRotation = Quaternion.identity;
                    Destroy(other.gameObject.transform.parent.gameObject);
                    object[] parameters;
                    switch (countMaxColliders)
                    {
                        case 0:
                            parameters = new object[] { currInt, 0, Time.time };
                            RecordObj.SendMessage("AttemptMistake", parameters);
                            RecordObj.SendMessage("printRecord");
                            break;
                        case 1:
                            parameters = new object[] { currInt, 1, Time.time };
                            RecordObj.SendMessage("AttemptMistake", parameters);
                            RecordObj.SendMessage("printRecord");
                            break;
                        default:
                            parameters = new object[] { currInt, 2, Time.time };
                            RecordObj.SendMessage("AttemptMistake", parameters);
                            RecordObj.SendMessage("printRecord");
                            break;

                    }
                }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        Debug.Log("leaveWorkstation:");
        //Debug.Log(other.gameObject.transform.parent.name);
        Debug.Log(other.name);
        Debug.Log(other.transform.position);
        if (other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().assemblePhase == ComponentState.AssemblePhase.PairingA)
        {
            //Debug.Log("TestOb");
            //Instantiate(testOb,other.transform.position,other.transform.rotation);
            if (other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().capturedObject != null&& other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().capturedObject.GetComponent<ComponentState>().assemblePhase != ComponentState.AssemblePhase.Insertion)
            {
                other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().capturedObject.GetComponent<ComponentState>().PhaseChange(ComponentState.AssemblePhase.Identification);
                other.gameObject.transform.parent.gameObject.SetActive(false);
                other.gameObject.transform.localPosition = Vector3.zero;
                other.gameObject.transform.localRotation = Quaternion.identity;
                //Destroy(other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().capturedObject);
                countCurrColliders--;
            }
        }
            other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().PhaseChange(ComponentState.AssemblePhase.Identification);
            countCurrColliders--;
    }

    public void checkMode()
    {
         if(WorkMode!=1)
        {
            GameObject[] foundGameObjects = GameObject.FindGameObjectsWithTag("Part");
            foreach (GameObject foundObject in foundGameObjects)
            {
                Debug.Log("Find parent: " + foundObject.name);
                MeshRenderer[] foundObj = foundObject.transform.Find("Throwable(Clone)").Find("Sockets").GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer obj in foundObj)
                {
                    Debug.Log("Find render: " + obj.gameObject.name);
                    obj.enabled = false;
                }
                LineRenderer[] foundLine = foundObject.transform.Find("Throwable(Clone)").Find("Sockets").GetComponentsInChildren<LineRenderer>();
                foreach (LineRenderer line in foundLine)
                {
                    Debug.Log("Find line: " + line.gameObject.name);
                    line.enabled = false;
                }

            }
        }
    }


    //void FixedUpdate()
    //    {

    //    }

        // if(other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().assemblePhase == ComponentState.AssemblePhase.Insertion )
        // {
        //     other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().PhaseChange(ComponentState.AssemblePhase.Identification); 
        // }
    }



