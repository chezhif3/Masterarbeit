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
    public GameObject colliderMeshPrefab;
    public GameObject socketPrefab;
    public GameObject throwable;
    public GameObject linearDrive;
    public GameObject capturedObject;
    public GameObject Spawner;
    public Transform spawnTransform;
    public int Index;

    public bool isPairing;


    private bool ifInsert;
    // public GameObject childColliders = new GameObject("childColliders");
    // public GameObject childSockets = new GameObject("childSockets");



    //private int index = (int)assemblePhase;

    void Start()
    {
        initialComponent();

        switch (assemblePhase)
        {
            case AssemblePhase.Identification:
                transform.Find("Throwable(Clone)").gameObject.SetActive(true);
                transform.Find("Throwable(Clone)").Find("Colliders").gameObject.SetActive(true);
                transform.Find("Throwable(Clone)").Find("Sockets").gameObject.SetActive(false);
                transform.Find("LinearDrive(Clone)").gameObject.SetActive(false);
                collidersUpdate();
                transform.Find("Throwable(Clone)").gameObject.GetComponent<Rigidbody>().useGravity = true;
                assemblePhase = AssemblePhase.Identification;
                break;
            case AssemblePhase.Pairing:
                transform.Find("Throwable(Clone)").gameObject.SetActive(true);
                transform.Find("Throwable(Clone)").Find("Colliders").gameObject.SetActive(true);
                transform.Find("Throwable(Clone)").Find("Sockets").gameObject.SetActive(true);
                transform.Find("LinearDrive(Clone)").gameObject.SetActive(false);
                collidersUpdate();
                transform.Find("Throwable(Clone)").gameObject.GetComponent<Rigidbody>().useGravity = false;
                assemblePhase = AssemblePhase.Pairing;
                break;
            case AssemblePhase.Insertion:
                transform.Find("LinearDrive(Clone)").gameObject.SetActive(true);
                assemblePhase = AssemblePhase.Insertion;
                break;
        }
    }

    private void collidersUpdate()
    {
        for (int i = 0; i < transform.Find("Throwable(Clone)").Find("Colliders").childCount; i++)
        {
            Destroy(transform.Find("Throwable(Clone)").Find("Colliders").GetChild(i).gameObject);
        }
        for (int i = 0; i < transform.Find("Throwable(Clone)").Find("Sockets").childCount; i++)
        {
            Destroy(transform.Find("Throwable(Clone)").Find("Sockets").GetChild(i).gameObject);
        }
        for (int i = 0; i < transform.Find("LinearDrive(Clone)").Find("Colliders").childCount; i++)
        {
            Destroy(transform.Find("LinearDrive(Clone)").Find("Colliders").GetChild(i).gameObject);
        }
        colliderMeshPrefab = transform.Find("Colliders").gameObject;
        socketPrefab = transform.Find("Sockets").gameObject;
        Debug.Log("spwan mesh update");
        Debug.Log(colliderMeshPrefab.name);
        Instantiate(colliderMeshPrefab, transform.Find("Throwable(Clone)").Find("Colliders"));
        if (socketPrefab != null)
        {
            Instantiate(socketPrefab, transform.Find("Throwable(Clone)").Find("Sockets"));
        }
        Instantiate(colliderMeshPrefab, transform.Find("LinearDrive(Clone)").Find("Colliders"));

        if(transform.Find("Throwable(Clone)").Find("Colliders").gameObject.activeSelf)
        {
            GameObject currOb=transform.Find("Throwable(Clone)").Find("Colliders").gameObject;
            for (int i = 0; i < currOb.transform.childCount; i++)
            {
                currOb.transform.GetChild(i).gameObject.SetActive(true);
                Debug.Log(transform.Find("Throwable(Clone)").Find("Colliders").GetChild(i).gameObject.name);
            }
        }
        else
        {
            GameObject currOb=transform.Find("Throwable(Clone)").Find("Colliders").gameObject;
            for (int i = 0; i < currOb.transform.childCount; i++)
            {
                currOb.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
         if(transform.Find("Throwable(Clone)").Find("Sockets").gameObject.activeSelf)
        {
            GameObject currOb=transform.Find("Throwable(Clone)").Find("Sockets").gameObject;
            for (int i = 0; i < currOb.transform.childCount; i++)
            {
                currOb.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        else
        {
            GameObject currOb=transform.Find("Throwable(Clone)").Find("Sockets").gameObject;
            for (int i = 0; i < currOb.transform.childCount; i++)
            {
                currOb.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        if(transform.Find("LinearDrive(Clone)").Find("Colliders").gameObject.activeSelf)
        {
            GameObject currOb=transform.Find("LinearDrive(Clone)").Find("Colliders").gameObject;
            for (int i = 0; i < currOb.transform.childCount; i++)
            {
                currOb.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        else
        {
            GameObject currOb=transform.Find("LinearDrive(Clone)").Find("Colliders").gameObject;
            for (int i = 0; i < currOb.transform.childCount; i++)
            {
                currOb.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        Debug.Log(name);
        Debug.Log("ColliderUpdated");
        Debug.Log(transform.Find("Throwable(Clone)").Find("Colliders").GetChild(0).gameObject.name);

    }

    public void SetStartEnd(Transform start, Transform end)
    {
        Debug.Log("Passing StartEnd");
        Debug.Log(transform.Find("LinearDrive(Clone)").gameObject.name);
        Debug.Log(start.name);
        transform.Find("LinearDrive(Clone)").Find("Start").SetParent(start);
        transform.Find("LinearDrive(Clone)").Find("End").SetParent(end);
      //  Debug.Log(transform.Find("LinearDrive").gameObject.GetComponent<Valve.VR.InteractionSystem.LinearDrive>().startPosition.position);
    }

    public void PhaseChange(AssemblePhase phase)
    {
        Vector3 _position = Vector3.zero;
        Quaternion _rotation = Quaternion.identity;
        for (int i = 0; i < transform.childCount; i++)
        {
            int activeCount=0;
            if (transform.GetChild(i).gameObject.activeSelf == true)
            {
                activeCount++;
                if(activeCount>1)
                {
                    Debug.Log("Error, 2 phases at the same time");
                    Debug.Log(transform.GetChild(i).gameObject.name);
                    break;
                }
                _position = transform.GetChild(i).position;
                _rotation = transform.GetChild(i).rotation;
            }
        }
        if (assemblePhase != phase)
        {
            switch (phase)
            {   
                case AssemblePhase.Identification:
                    //initialComponent();
                    transform.Find("Throwable(Clone)").position = _position;
                    transform.Find("Throwable(Clone)").rotation = _rotation;
                    transform.Find("Throwable(Clone)").gameObject.SetActive(true);
                    transform.Find("Throwable(Clone)").Find("Colliders").gameObject.SetActive(true);
                    transform.Find("Throwable(Clone)").Find("Sockets").gameObject.SetActive(false);
                    transform.Find("LinearDrive(Clone)").gameObject.SetActive(false);
                    break;
                case AssemblePhase.Pairing:
                    transform.Find("Throwable(Clone)").position = _position;
                    transform.Find("Throwable(Clone)").rotation = _rotation;
                    transform.Find("Throwable(Clone)").gameObject.SetActive(true);
                    transform.Find("Throwable(Clone)").Find("Colliders").gameObject.SetActive(true);
                    transform.Find("Throwable(Clone)").Find("Sockets").gameObject.SetActive(true);
                    transform.Find("LinearDrive(Clone)").gameObject.SetActive(false);
                    break;
                case AssemblePhase.Insertion:
                    transform.Find("Throwable(Clone)").gameObject.SetActive(false);
                    transform.Find("Throwable(Clone)").Find("Colliders").gameObject.SetActive(false);
                    transform.Find("Throwable(Clone)").Find("Sockets").gameObject.SetActive(false);
                    transform.Find("LinearDrive(Clone)").position = _position;
                    transform.Find("LinearDrive(Clone)").rotation = _rotation;
                    transform.Find("LinearDrive(Clone)").gameObject.SetActive(true);
                    break;
            }
        }
        collidersUpdate();
        assemblePhase = phase;
        // Debug.Log(phase);
        // Debug.Log(assemblePhase);
    }

    public void AddCollider(GameObject _gameObject,Vector3 positon, Quaternion rotation)
    {
        Instantiate(_gameObject,positon,rotation,transform.Find("Colliders"));
        collidersUpdate();
    }

    public void AddSockets(GameObject _gameObject,Vector3 positon, Quaternion rotation)
    {
        Instantiate(_gameObject,positon,rotation,transform.Find("Sockets"));
        collidersUpdate();
    }

    private void initialComponent()
    {
        colliderMeshPrefab = transform.Find("Colliders").gameObject;
        socketPrefab = transform.Find("Sockets").gameObject;
        if(transform.Find("Throwable(Clone)")!= null)
        {
            Destroy(transform.Find("Throwable(Clone)").gameObject);
            Instantiate(throwable,transform);
        }
        else
        {
            Instantiate(throwable,transform);
        }
        if(transform.Find("LinearDrive(Clone)")!= null)
        {
            Debug.Log("destroy linear drive");
            Destroy(transform.Find("LinearDrive(Clone)").gameObject);
            Instantiate(linearDrive,transform);
        }
        else
        {
            Debug.Log("spawn linear drive");
            Instantiate(linearDrive,transform);
        }

    //    Debug.Log("spawn mesh");
    //    Instantiate(colliderMeshPrefab, transform.Find("Throwable(Clone)").Find("Colliders"));
    //    if (socketPrefab != null)
    //    {
    //        var newOG = Instantiate(socketPrefab, transform.Find("Throwable(Clone)").Find("Sockets"));
    //        newOG.SetActive(false);
    //    }
    //    Instantiate(colliderMeshPrefab, transform.Find("LinearDrive(Clone)").Find("Colliders"));
    }

    void OnDestroy()
    {
        Debug.Log("Respawn");
        Spawner.SendMessage("spawn");
    }
}

