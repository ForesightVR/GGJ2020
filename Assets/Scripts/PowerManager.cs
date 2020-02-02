using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    int currentPanel;
    int disabledRings;
    void Start()
    {
        StartPanel(0);
    }

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
            doorAnimator.SetTrigger("Open");
            Debug.Log("Puzzle Complete");
        }
    }

    public void RingDisabled()
    {
        Debug.Log("Ring Disabled");
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

    public void StartPanel(int panelIndex)
    {
        powerPanels[panelIndex].Init(this);
    }

    public void EndPanel()
    {
        ActivateNextPanel();
    }
}
