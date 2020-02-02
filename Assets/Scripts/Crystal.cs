using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public float timeToOverwhelm;
    public MeshRenderer[] meshRenderers;
    public float lerpSpeed = .001f;
    public PowerManager powerManager;

    public GameObject[] dummies;
    public GameObject[] shards;

    Color originalColor;
    Coroutine overheatCrystal;
    bool overheating;

    AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();
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
        source.Play();
        powerManager.PowerDisabled();
        powerManager.SetPowerRoomActive(true);

        foreach (GameObject dummy in dummies)
            dummy.SetActive(false);

        foreach (GameObject shard in shards)
            shard.SetActive(true);
    }
}
