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

    private ListNode head;
    private ListNode curr;

    public Transform center;
    public int WorkMode=1;

    private int countMaxColliders;
    public int countCurrColliders=0;

    public GameObject testOb;

    public GameObject RecordObj;

    public int testNr = 0;

    void Start()
    {
        head = new ListNode();
        head.value = new int[] {5,0};
        curr = head;

        // ListNode node1 = new ListNode();
        // node1.value =  new int[] {1,2};

        // ListNode node2 = new ListNode();
        // node2.value =  new int[] {1,3};

        // ListNode node3 = new ListNode();
        // node3.value =  new int[] {3,5};

        // ListNode node4 = new ListNode();
        // node4.value =  new int[] {3,4};

    }

    void NextStep()
    {
        curr = curr.next;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().assemblePhase==ComponentState.AssemblePhase.Identification && !other.gameObject.GetComponent<Valve.VR.InteractionSystem.Throwable>().attached)
        {
            if (WorkMode == 1)
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
                        other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().OpenSocket(curr.value[0]);
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
            if (WorkMode == 2)
            {
                int currInt = other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().Index;
                int query = System.Array.IndexOf(curr.value, other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().Index);
                Debug.Log("enterWorkstation:");
                Debug.Log(other.gameObject.transform.parent.name);
                Debug.Log("Index: " + query);
                // if(!other.gameObject.transform.parent.parent.parent.gameObject.GetComponent<Valve.VR.InteractionSystem.Throwable>().attached)
                // { 
                if (countCurrColliders >= 2)
                {
                    Destroy(other.gameObject.transform.parent.gameObject);
                }
                else
                {
                    if (query != -1)
                    {
                        if (query == 0)
                        {
                            other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().PhaseChange(ComponentState.AssemblePhase.PairingA);
                            countCurrColliders++;
                        }
                        if (query == 1)
                        {
                            other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().PhaseChange(ComponentState.AssemblePhase.PairingB);
                            countCurrColliders++;
                        }
                        //if (other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().assemblePhase == ComponentState.AssemblePhase.Identification)
                        //{
                        //    other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().PhaseChange(ComponentState.AssemblePhase.Pairing);
                        //    countCurrColliders++;
                        //}
                    }
                    else
                    {
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

                        }
                    }

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
            Debug.Log("TestOb");
            Instantiate(testOb,other.transform.position,other.transform.rotation);
            if (other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().capturedObject != null&& other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().capturedObject.GetComponent<ComponentState>().assemblePhase != ComponentState.AssemblePhase.Insertion)
            {
                other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().capturedObject.GetComponent<ComponentState>().PhaseChange(ComponentState.AssemblePhase.Identification);
                Destroy(other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().capturedObject);
                countCurrColliders--;
            }
        }
            other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().PhaseChange(ComponentState.AssemblePhase.Identification);
            countCurrColliders--;
    }


    //void FixedUpdate()
    //    {

    //    }

        // if(other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().assemblePhase == ComponentState.AssemblePhase.Insertion )
        // {
        //     other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().PhaseChange(ComponentState.AssemblePhase.Identification); 
        // }
    }



