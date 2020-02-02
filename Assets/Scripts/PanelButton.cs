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
        animator = GetComponentInParent<Animator>();
    }

    public void Init(PowerPanel parentPanel,  string _symbol) 
    {
        symbol = GetComponentInChildren<TextMeshProUGUI>();
        this.parentPanel = parentPanel;
        symbol.text = _symbol;

        Debug.Log(symbol.text + " --- " + _symbol);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Finger"))
        {
            parentPanel.CheckButton(symbol.text);
            animator.SetTrigger("Pressed");
        }
    }
}
