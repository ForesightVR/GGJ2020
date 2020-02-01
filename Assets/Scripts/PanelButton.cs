using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelButton : MonoBehaviour
{
    TextMeshProUGUI symbol;
    PowerPanel parentPanel;

    public void Init(PowerPanel parentPanel,  string symbol) 
    {
        this.symbol = GetComponentInChildren<TextMeshProUGUI>();
        this.parentPanel = parentPanel;
        this.symbol.text = symbol;
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.tag.Equals("Finger"))
        {
            Debug.Log("Triggered by " + other.name + " symbol: " + symbol.text);
            parentPanel.CheckButton(symbol.text);
        }
    }
}
