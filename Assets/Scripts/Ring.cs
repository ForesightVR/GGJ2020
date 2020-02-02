using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public float timeToOverwhelm;
    public MeshRenderer[] meshRenderers;
    public PowerManager powerManager;

    Color originalColor;
    Coroutine overheatRing;
    bool overheating;
    bool disabled;
    AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        originalColor = meshRenderers[0].material.color;
    }

    private void Update()
    {
        if (disabled) return;
        if (overheating)
        {
            foreach (MeshRenderer rend in meshRenderers)
                rend.material.color = Color.Lerp(rend.material.color, Color.red, .25f * Time.deltaTime);
        }
    }

    public void StartOverheat()
    {
        if (disabled) return;
        overheating = true;

        if(overheatRing == null)
            overheatRing = StartCoroutine(Overheating());
    }

    public void StopOverheat()
    {
        if (disabled) return;
        overheating = false;
            StopAllCoroutines();
        if (overheatRing != null)
            overheatRing = null;
    }

    IEnumerator Overheating()
    {
        yield return new WaitForSeconds(timeToOverwhelm);
        Disable();
    }

    public void Disable()
    {
        source.Play();
        foreach (MeshRenderer rend in meshRenderers)
            rend.material.color = Color.black;
        disabled = true;
        powerManager.RingDisabled();
    }

}
