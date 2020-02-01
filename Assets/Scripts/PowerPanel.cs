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

    public GameObject panelBtnArea;
    public TextMeshProUGUI symbolArea;
    public Animator animator;
    public MeshRenderer buttonRenderer;
    public PowerPanelButton panelButton;

    int symbolIndex;
    char[] possibleSymbols;
    List<string> generatedSymbols = new List<string>();
    PowerManager powerManager;
    List<GameObject> buttons = new List<GameObject>();

    public void Init(PowerManager powerManager)
    {
        symbolIndex = 0;
        animator.SetTrigger("Open");
        this.powerManager = powerManager;
       
        //Generate Symbols
        possibleSymbols = powerManager.possibleSymbols.ToCharArray().GetRandomItems(panelBtnAmt);

        GenerateSymbols();

        //Generate Buttons
        float panelAreaWidth = panelBtnArea.transform.localScale.x / offsetScale;
        float incrementValue = panelAreaWidth / (panelBtnAmt - 1);

        float startingValue = -(panelAreaWidth / 2);
        for (int i = 0; i < panelBtnAmt; i++)
        {
            var spawnedObj = Instantiate(powerManager.panelButton, panelBtnArea.transform);
            spawnedObj.transform.localPosition = new Vector2(startingValue, 0);
            spawnedObj.GetComponent<PanelButton>().Init(this, possibleSymbols[i].ToString());
            buttons.Add(spawnedObj);
            startingValue += incrementValue;
        }
    }

    public void PrepareButton()
    {
        buttonRenderer.material.color = Color.green;
        panelButton.Init(this);
    }

    void GenerateSymbols()
    {
        generatedSymbols.Clear();
        symbolArea.text = "";
        for (int i = 0; i < symbolAmt; i++)
        {
            generatedSymbols.Add(possibleSymbols[Random.Range(0, possibleSymbols.Length - 1)].ToString());
            symbolArea.text += generatedSymbols[i];
        }
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
        foreach (GameObject go in buttons)
            Destroy(go);
        buttons.Clear();
    }

    public void TurnOnPanel()
    {
        //lightningEffect

    }
}
