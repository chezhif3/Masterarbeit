using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<screw>().ifReady&& !other.gameObject.GetComponent<screw>().ifDone)
        {
            transform.parent.parent.parent.parent.gameObject.GetComponent<Valve.VR.InteractionSystem.Interactable>().attachedToHand.TriggerHapticPulse(1, 100, 0.5f);
            other.transform.position = other.transform.position - 0.01f * other.transform.right;
            other.gameObject.GetComponent<screw>().ifDone = true;
        }
        
    }
}
