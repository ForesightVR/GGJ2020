using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public float timeToOverwhelm;
    public MeshRenderer[] meshRenderers;
    public float lerpSpeed = .001f;
    public PowerManager powerManager;

    Color originalColor;
    Coroutine overheatCrystal;
    bool overheating;

    private void Start()
    {
        originalColor = meshRenderers[0].material.color;
    }

    private void Update()
    {
        if(overheating)
        {
            foreach (MeshRenderer rend in meshRenderers)
            {
                rend.material.color = Color.Lerp(rend.material.color, Color.red, lerpSpeed * Time.deltaTime);
            }
        }
        //else
        //{
        //    foreach (MeshRenderer rend in meshRenderers)
        //        rend.material.color = Color.Lerp(rend.material.color, originalColor, .1f * Time.deltaTime);
        //}
    }

    public void StartOverheat()
    {
        overheating = true;
        overheatCrystal = StartCoroutine(Overheating());
    }

    public void StopOverheat()
    {
        overheating = false;
        if (overheatCrystal != null)
            StopCoroutine(overheatCrystal);
    }

    IEnumerator Overheating()
    {
        yield return new WaitForSeconds(timeToOverwhelm);
        Explode();
    }

    public void Explode()
    {
       // GetComponent<MeshRenderer>().material.color = Color.red;
        Debug.Log("Kaboom!");
        powerManager.PowerDisabled();
        powerManager.SetPowerRoomActive(true);
    }


}
