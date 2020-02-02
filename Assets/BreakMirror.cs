using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakMirror : MonoBehaviour
{
    public GameObject brokenModel;
    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Finger"))
        {
            source.Play();
            brokenModel.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
