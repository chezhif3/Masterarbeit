using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentState : MonoBehaviour
{
    public enum AssemblePhase
    {
        Pairing = 0, // The object should snap to the position of the specified attachment point on the hand.
        Insertion = 1, // Other objects attached to this hand will be detached.
    };
    public AssemblePhase assemblePhase;
    //private Transform oldPosition;
    //private Transform newPosition;

    //private int index = (int)assemblePhase;

    void Start()
    {
        //Debug.Log("Start");
        switch (assemblePhase)
        {
            case AssemblePhase.Pairing :
                //Debug.Log("Pairing");
                //Debug.Log(name);
                transform.Find("Throwable").gameObject.SetActive(true);
                break;
            case AssemblePhase.Insertion:
                transform.Find("LinearDrive").gameObject.SetActive(true);
                break;
        }
    }

   public void SetStartEnd(Transform start, Transform end)
    {
        Debug.Log(transform.Find("LinearDrive").gameObject.name);
        transform.Find("LinearDrive").gameObject.GetComponent<Valve.VR.InteractionSystem.LinearDrive>().startPosition = start;
        transform.Find("LinearDrive").gameObject.GetComponent<Valve.VR.InteractionSystem.LinearDrive>().endPosition = end;
    }
}
