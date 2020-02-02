using System;
using UnityEngine;

public class CrystalMaker : MonoBehaviour
{
    private int NUM_SHARDS = 3;
    private int numShardsInMaker = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("CrystalShard"))
        {
            return;
        }

        if (++numShardsInMaker == NUM_SHARDS)
        {
            var pos = transform.position;
            
            // Instantiate(SuperShard)
        } 
    }

    private void Update()
    {
        Debug.Log(numShardsInMaker);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("CrystalShard"))
        {
            return;
        }

        numShardsInMaker--;
    }
}
