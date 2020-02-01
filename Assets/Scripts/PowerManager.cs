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
        powerPanels[0].PrepareButton();
    }

    public void ActivateNextPanel() 
    {
        if(currentPanel < powerPanels.Count - 1)
        {
            powerPanels[currentPanel].gameObject.SetActive(false);
            currentPanel++;
            powerPanels[currentPanel].PrepareButton();
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
        for(int i = 0; i < powerPanels.Count; i++)
        {
            if (powerPanels[i] == panel)
                return i;
        }
        return 0;
    }

    public void EndPanel()
    {
        ActivateNextPanel();
    }
}
