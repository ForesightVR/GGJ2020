using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OuterRimStudios.Utilities;
using TMPro;

public class PowerPanel : MonoBehaviour
{
    public int symbolAmt;
    public int panelBtnAmt;
    public float offsetScale;
    public float timer = 5f;

    public GameObject panelBtnArea;
    public TextMeshProUGUI symbolArea;    
    public MeshRenderer buttonRenderer;
    public PowerPanelButton panelButton;
    
    List<string> generatedSymbols = new List<string>();
    public List<GameObject> buttons = new List<GameObject>();

    Animator animator;
    PowerManager powerManager;
    PowerPanelButton powerBtn;
    Coroutine turnOffCoroutine;

    int symbolIndex;
    char[] possibleSymbols;

    private void Awake()
    {
        symbolArea.gameObject.SetActive(false);
        animator = GetComponent<Animator>();
        powerBtn = buttonRenderer.gameObject.GetComponent<PowerPanelButton>();
    }

    public void Init(PowerManager powerManager)
    {
        symbolIndex = 0;        
        this.powerManager = powerManager;    

        GenerateSymbols();
        MatchButtonsToSymbols();
        PrepareButton();
    }

    public void PrepareButton()
    {
        buttonRenderer.material.color = Color.green;
        panelButton.Init(this);
    }

    void GenerateSymbols()
    {
        possibleSymbols = powerManager.possibleSymbols.ToCharArray().GetRandomItems(panelBtnAmt);

        generatedSymbols.Clear();
        symbolArea.text = "";
        for (int i = 0; i < symbolAmt; i++)
        {
            generatedSymbols.Add(possibleSymbols[Random.Range(0, possibleSymbols.Length - 1)].ToString());
            symbolArea.text += generatedSymbols[i];
        }

        symbolArea.gameObject.SetActive(true);
    }

    void MatchButtonsToSymbols()
    {
        int index = 0;
        foreach (GameObject go in buttons)
        {
            go.GetComponent<PanelButton>().Init(this, possibleSymbols[index].ToString());
            index++;
        }
    }
    public void TurnOnPanel()
    {
        //lightningEffect
        powerBtn.GetComponent<Collider>().enabled = false;
        animator.SetTrigger("Open");
        symbolArea.gameObject.SetActive(false);

        foreach (GameObject button in buttons)
        {
            button.GetComponent<Collider>().enabled = true;
        }

        turnOffCoroutine = StartCoroutine(TurnOffPanel());
    }

    IEnumerator TurnOffPanel()
    {
        yield return new WaitForSeconds(timer);
        Failed();
    }

    public void CheckButton(string symbol)
    {
        if (symbolIndex >= generatedSymbols.Count) return;

        foreach (string s in generatedSymbols)
            Debug.Log(s);

        if (generatedSymbols[symbolIndex] == symbol)
        {
            if(symbolIndex < generatedSymbols.Count -1)
            {
                symbolIndex++;
            }
            else
            {
                powerManager.EndPanel();
            }
        }
        else
        {
            Failed();
        }
    }

    void Failed()
    {
        StopCoroutine(turnOffCoroutine);
        symbolIndex = 0;
        powerBtn.GetComponent<Collider>().enabled = true;
        GenerateSymbols();
        MatchButtonsToSymbols();
        animator.SetTrigger("Close");

        foreach (GameObject button in buttons)
        {
            button.GetComponent<Collider>().enabled = false;
        }
    }
}
