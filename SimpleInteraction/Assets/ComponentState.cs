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
    public AssemblePhase assemblePhase = 0;
    private Transform oldPosition;
}
