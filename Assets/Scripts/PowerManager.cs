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
            //StartPanel(currentPanel);
        }
        else
        {
            powerPanels[currentPanel].gameObject.SetActive(false);
            Debug.Log("Puzzle Complete");
            //PuzzleComplete();
        }
    }

    public void StartPanel(int panelIndex)
    {
        powerPanels[panelIndex].Init(this);
    }

    public int GetIndex(PowerPanel panel)
    {
        return powerPanels.IndexOf(panel);

        /*for(int i = 0; i < powerPanels.Count; i++)
        {
            if (powerPanels[i] == panel)
                return i;
        }
        return 0;*/
    }

    public void EndPanel()
    {
        ActivateNextPanel();
    }
}
