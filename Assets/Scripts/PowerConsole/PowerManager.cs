using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PowerManager : MonoBehaviour
{
    public List<PowerPanel> powerPanels;
    public GameObject panelButton;

    public string possibleSymbols;
    int currentPanel;
    void Start()
    {
        StartPanel(0);
    }

    public void ActivateNextPanel() 
    {
        if(currentPanel < powerPanels.Count - 1)
        {           
            powerPanels[currentPanel].gameObject.SetActive(false);
            currentPanel++;
            StartPanel(currentPanel);
        }
        else
        {
            powerPanels[currentPanel].gameObject.SetActive(false);
            Debug.Log("Puzzle Complete");
        }
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
