using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureOn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enterWorkstation:");
        Debug.Log(other.gameObject.transform.parent.name);
        // if(!other.gameObject.transform.parent.parent.parent.gameObject.GetComponent<Valve.VR.InteractionSystem.Throwable>().attached)
        // { 
        if(other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().assemblePhase == ComponentState.AssemblePhase.Identification )
        {
            other.gameObject.transform.parent.gameObject.GetComponent<ComponentState>().PhaseChange(ComponentState.AssemblePhase.Pairing); 
        }
        // }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("leaveWorkstation:");
        Debug.Log(other.gameObject.transform.parent.name);
        if(other.gameObject.transform.parent.parent.gameObject.GetComponent<ComponentState>().assemblePhase == ComponentState.AssemblePhase.Pairing )
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

    IEnumerator delayChange(GameObject _gameObject)
    {
        yield return new WaitForFixedUpdate();
        _gameObject.GetComponent<ComponentState>().PhaseChange(ComponentState.AssemblePhase.Identification);
    }
}
