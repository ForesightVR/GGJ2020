using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public float timeToOverwhelm;
    public MeshRenderer[] meshRenderers;

    List<Rigidbody> rbs = new List<Rigidbody>();
    List<Collider> colliders = new List<Collider>();

    Coroutine overheatCrystal;
    bool overheating;

    private void Start()
    {
        foreach(MeshRenderer mesh in meshRenderers)
        {
            rbs.Add(mesh.GetComponent<Rigidbody>());
            colliders.Add(mesh.GetComponent<Collider>());
        }
    }

    public void StartOverheat()
    {
        Debug.Log("Start Overheat");
        overheating = true;
        overheatCrystal = StartCoroutine(Overheating());
    }

    public void StopOverheat()
    {
        Debug.Log("Stop Overheat");
        overheating = false;

        if (overheatCrystal != null)
            StopCoroutine(overheatCrystal);
    }

    private void Update()
    {
        if(overheating)
            foreach (MeshRenderer rend in meshRenderers)
            {
                rend.material.color = Color.Lerp(rend.material.color, Color.red, .75f * Time.deltaTime);
               // rend.material.SetColor("_EmissionColor", Color.Lerp(rend.material.GetColor("_EmissionColor"), Color.red, .0001f *  Time.deltaTime));
            }
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

        foreach (Rigidbody rb in rbs)
            rb.useGravity = true;
        foreach (Collider col in colliders)
            col.enabled = true;

    }


}
