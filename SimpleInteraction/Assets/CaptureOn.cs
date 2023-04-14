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

    void Start()
    {
        head = new ListNode();
        head.value = new int[] {1,2};
        curr = head;
    }

    void NextStep()
    {
        curr = curr.next;
    }

    private void OnTriggerEnter(Collider other)
    {
        int currInt = other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().Index;
        int query = System.Array.IndexOf(curr.value, other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().Index);
        Debug.Log("enterWorkstation:");
        Debug.Log(other.gameObject.transform.parent.name);
        // if(!other.gameObject.transform.parent.parent.parent.gameObject.GetComponent<Valve.VR.InteractionSystem.Throwable>().attached)
        // { 
        if(query != -1)
        {
            if(other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().assemblePhase == ComponentState.AssemblePhase.Identification )
            {
                other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().PhaseChange(ComponentState.AssemblePhase.Pairing); 
            }
        }
        else
        {
            Destroy(other.gameObject.transform.parent.gameObject);
            GameObject targetObject= GameObject.Find("Profiling");
            object[] parameters = new object[] {currInt,0,Time.time};
            targetObject.SendMessage("recordAttempt",parameters);
            targetObject.SendMessage("printRecord");
        }

        // }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("leaveWorkstation:");
        Debug.Log(other.gameObject.transform.parent.name);
        if(other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().assemblePhase == ComponentState.AssemblePhase.Pairing )
        {
            if(other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().capturedObject!=null)
                {
                other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().capturedObject.GetComponent<ComponentState>().PhaseChange(ComponentState.AssemblePhase.Identification);
                Destroy(other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().capturedObject);
                }
            other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().PhaseChange(ComponentState.AssemblePhase.Identification); 
        }

        // if(other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().assemblePhase == ComponentState.AssemblePhase.Insertion )
        // {
        //     other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().PhaseChange(ComponentState.AssemblePhase.Identification); 
        // }
    }


}
