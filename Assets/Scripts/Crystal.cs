using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public float timeToOverwhelm;

    Coroutine overheatCrystal;

    public void StartOverheat()
    {
        Debug.Log("Start Overheat");
        overheatCrystal = StartCoroutine(Overheating());
    }

    public void StopOverheat()
    {
        Debug.Log("Stop Overheat");
        if (overheatCrystal != null)
            StopCoroutine(overheatCrystal);
    }

    IEnumerator Overheating()
    {
        Debug.Log("Over Heating");
        yield return new WaitForSeconds(timeToOverwhelm);
        Explode();
    }

    public void Explode()
    {
       // GetComponent<MeshRenderer>().material.color = Color.red;
        Debug.Log("Kaboom!");
    }


}
