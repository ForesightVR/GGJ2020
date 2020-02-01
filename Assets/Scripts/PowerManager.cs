using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(currentPanel <= powerPanels.Count)
        {
            currentPanel++;
            StartPanel(currentPanel);
        }
        else
        {
            //PuzzleComplete();
        }
    }

    void StartPanel(int panelIndex)
    {
        powerPanels[panelIndex].Init(this);
    }

    public void EndPanel() { }
}
