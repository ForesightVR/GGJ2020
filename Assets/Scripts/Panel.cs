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
        for (var i = 0; i < 20; i++)
        {
            var position = transform.position;
            var x = position.x + Random.Range(-1, 1);
            var y = position.y + Random.Range(2, 5);
            var z = position.z + Random.Range(0, 1);

            Instantiate(vacuumTubePrefab, new Vector3(x, y, z), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
