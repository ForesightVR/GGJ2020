using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelButton : MonoBehaviour
{
    TextMeshProUGUI symbol;
    PowerPanel parentPanel;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

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
            parentPanel.CheckButton(symbol.text);
            animator.SetTrigger("pressed");
        }
    }
}
