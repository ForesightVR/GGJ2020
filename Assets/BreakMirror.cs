using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakMirror : MonoBehaviour
{
    public GameObject brokenModel;
    public AudioSource source;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Finger"))
        {
            brokenModel.SetActive(true);
            source.Play();
            gameObject.SetActive(false);
        }
    }
}
