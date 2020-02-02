using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpButton : MonoBehaviour
{
    public GameObject warpEffect;
    bool active;

    public void Activate()
    {
        active = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Finger") && active)
        {
            warpEffect.SetActive(true);
            active = false;
        }
    }
}
