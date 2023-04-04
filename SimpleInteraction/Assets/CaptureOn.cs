using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureOn : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.transform.parent.gameObject.name);
        if(!other.gameObject.GetComponent<Valve.VR.InteractionSystem.Throwable>().attached)
        { 
            other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().PhaseChange(ComponentState.AssemblePhase.Pairing); 
        }
    }
}
