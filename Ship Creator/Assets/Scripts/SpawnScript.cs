using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public GameObject toSpawn;

    void Spawn()
    {

        Vector3 camera = Camera.main.transform.position;
        Instantiate(toSpawn, new Vector3(camera.x + 1, camera.y, camera.z + 1), Quaternion.identity);
    }

    private void Start()
    {
        Spawn();
    }
}
