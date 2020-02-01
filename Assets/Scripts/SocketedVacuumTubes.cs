using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class SocketedVacuumTubes : MonoBehaviour
{
    public void DestroyVacuumTubes()
    {
        foreach (Transform vacuumTube in transform)
        {
            Destroy(vacuumTube.gameObject);
        }
    }
}
