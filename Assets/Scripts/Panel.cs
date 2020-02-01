using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Panel : MonoBehaviour
{
    public GameObject sockets;
    public GameObject vacuumTubes;
    public GameObject vacuumTubePrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < 50; i++)
        {
            GenerateVacuumTube();
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        // GenerateVacuumTube();
    }

    private void GenerateVacuumTube()
    {
        var position = transform.position;
        var x = position.x + Random.value * 2 - 1;
        var y = position.y + Random.value + 1;
        var z = position.z + Random.value;

        var newTube = Instantiate(vacuumTubePrefab, new Vector3(x, y, z), Quaternion.identity);

        newTube.transform.SetParent(vacuumTubes.transform);
    }
}
