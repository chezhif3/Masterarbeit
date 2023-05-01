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

    private void reSpawn(GameObject obj)
    {
        Debug.Log("Spawn: " + objectToSpawn.name);
        var newObj = Instantiate(obj, transform.position, transform.rotation);
        newObj.SetActive(true);
    }


}
