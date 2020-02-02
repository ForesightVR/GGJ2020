using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPanelButton : MonoBehaviour
{
    PowerPanel powerPanel;
    bool isReady;
    public Animator animator;

    public void Init(PowerPanel powerPanel)
    {
        this.powerPanel = powerPanel;
        isReady = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isReady) return;
        if(other.tag.Equals("Finger"))
        {
            animator.SetTrigger("ActivateConsoleButton");
            powerPanel.TurnOnPanel();
        }
    }
}
