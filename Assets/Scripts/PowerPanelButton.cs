using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPanelButton : MonoBehaviour
{
    public PowerManager powerManager;

    PowerPanel powerPanel;
    bool isReady;

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
            powerManager.StartPanel(powerManager.GetIndex(powerPanel));
        }
    }
}
