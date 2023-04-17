using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;

    void Start()
    {
        spawn();
    }

    private void spawn()
    {
        Debug.Log("Spawn: "+objectToSpawn.name);
        Instantiate(objectToSpawn,transform.position,transform.rotation);
    }


}
