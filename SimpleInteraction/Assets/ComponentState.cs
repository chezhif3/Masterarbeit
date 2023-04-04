using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentState : MonoBehaviour
{
    public enum AssemblePhase
    {
        Identification = 0,
        Pairing = 1,
        Insertion = 2,
    };

    public AssemblePhase assemblePhase;



    //private int index = (int)assemblePhase;

    void Start()
    {
        //Debug.Log("Start");
        switch (assemblePhase)
        {
            case AssemblePhase.Identification:
                transform.Find("Throwable").gameObject.SetActive(true);
                assemblePhase = AssemblePhase.Identification;
                break;
            case AssemblePhase.Pairing:
                transform.Find("RayCast").gameObject.SetActive(true);
                assemblePhase = AssemblePhase.Pairing;
                break;
            case AssemblePhase.Insertion:
                transform.Find("LinearDrive").gameObject.SetActive(true);
                assemblePhase = AssemblePhase.Insertion;
                break;
        }
    }

    public void SetStartEnd(Transform start, Transform end)
    {
        Debug.Log(transform.Find("LinearDrive").gameObject.name);
        transform.Find("LinearDrive").gameObject.GetComponent<Valve.VR.InteractionSystem.LinearDrive>().startPosition = start;
        transform.Find("LinearDrive").gameObject.GetComponent<Valve.VR.InteractionSystem.LinearDrive>().endPosition = end;
    }

    public void PhaseChange(AssemblePhase phase)
    {
        Vector3 _position = Vector3.zero;
        Quaternion _rotation = Quaternion.identity;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf == true)
            {
                _position = transform.GetChild(i).position;
                _rotation = transform.GetChild(i).rotation;

            }
        }
        switch (phase)
        {
            case AssemblePhase.Identification:
                transform.Find("Throwable").position = _position;
                transform.Find("Throwable").rotation = _rotation;
                transform.Find("Throwable").gameObject.SetActive(true);
                transform.Find("RayCast").gameObject.SetActive(false);
                transform.Find("LinearDrive").gameObject.SetActive(false);
                break;
            case AssemblePhase.Pairing:
                transform.Find("Throwable").gameObject.SetActive(false);
                transform.Find("RayCast").position = _position;
                transform.Find("RayCast").rotation = _rotation;
                transform.Find("RayCast").gameObject.SetActive(true);
                transform.Find("LinearDrive").gameObject.SetActive(false);
                break;
            case AssemblePhase.Insertion:
                transform.Find("Throwable").gameObject.SetActive(false);
                transform.Find("RayCast").gameObject.SetActive(false);
                transform.Find("LinearDrive").position = _position;
                transform.Find("LinearDrive").rotation = _rotation;
                transform.Find("LinearDrive").gameObject.SetActive(true);
                break;
        }
    }
}

