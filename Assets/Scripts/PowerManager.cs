using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Valve.VR.InteractionSystem;

public class PowerManager : MonoBehaviour
{
    public List<PowerPanel> powerPanels;
    public GameObject panelButton;
    public Animator animator;

    public Material powerSoruceMaterial;
    public GameObject[] disabledPowerSources;
    public GameObject laserDoor;

    public Animator doorAnimator;

    [System.Serializable]
    public struct PoweredObjects
    {
        public MeshRenderer renderer;
        public Material originalMaterial;
    }

    public PoweredObjects[] poweredObjects;

    public string possibleSymbols;

    [Space]
    public TeleportArea cellTeleportArea;
    public TeleportPoint[] cellTeleportPoints;

    [Space]
    public TeleportArea powerRoomTeleportArea;
    public TeleportPoint[] powerRoomTeleportPoints;

    [Space]
    public TeleportArea cockpitTeleportArea;
    public TeleportPoint[] cockpitTeleportPoints;

    int currentPanel;
    int disabledRings;

    public void ActivateNextPanel() 
    {
        if(currentPanel < powerPanels.Count - 1)
        {           
           // powerPanels[currentPanel].gameObject.SetActive(false);
            currentPanel++;
            StartPanel(currentPanel);
        }
        else
        {
            powerPanels[currentPanel].gameObject.SetActive(false);
            PowerEnabled();
            SetCockpitActive(true);
            SetCellActive(false);
            doorAnimator.SetTrigger("Open");
        }
    }
    public void RingDisabled()
    {
        disabledRings++;
        
        if(disabledRings >= 4)
        {
            animator.SetTrigger("OpenDome");
        }
    }

    public void PowerDisabled()
    {
        laserDoor.SetActive(false);
        foreach (GameObject go in disabledPowerSources)
            go.SetActive(false);

        foreach(PoweredObjects poweredObject in poweredObjects)
            poweredObject.renderer.material = poweredObject.originalMaterial;
    }

    public void PowerEnabled()
    {
        laserDoor.SetActive(true);
        foreach (GameObject go in disabledPowerSources)
            go.SetActive(true);


        foreach (PoweredObjects poweredObject in poweredObjects)
            poweredObject.renderer.material = powerSoruceMaterial;
    }

    public void SetCellActive(bool state)
    {
        cellTeleportArea.locked = !state;

        foreach (TeleportPoint teleportPoint in cellTeleportPoints)
            teleportPoint.locked = !state;
    }

    public void SetPowerRoomActive(bool state)
    {
        powerRoomTeleportArea.locked = !state;

        foreach (TeleportPoint teleportPoint in powerRoomTeleportPoints)
            teleportPoint.locked = !state;
    }

    public void SetCockpitActive(bool state)
    {
        cockpitTeleportArea.locked = !state;

        foreach (TeleportPoint teleportPoint in cockpitTeleportPoints)
            teleportPoint.locked = !state;
    }

    public void StartPanel(int panelIndex)
    {
        powerPanels[panelIndex].Init(this);
    }

    public void EndPanel()
    {
        ActivateNextPanel();
    }
}
