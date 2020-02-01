﻿using System.Collections;
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

    int symbolIndex;
    char[] possibleSymbols;
    List<string> generatedSymbols = new List<string>();
    PowerManager powerManager;


    public void Init(PowerManager powerManager)
    {
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
            startingValue += incrementValue;
        }
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
            Debug.Log("Symbol matches");
            //they match and increment

            if(symbolIndex < generatedSymbols.Count -1)
            {
                Debug.Log("Increment");
                symbolIndex++;
            }
            else
                powerManager.EndPanel();
        }
        else
        {
            Debug.Log("Symbols don't match! " + "Selected Symbol: " + symbol + " Symbol I was looking for: " + generatedSymbols[symbolIndex]);
            symbolIndex = 0;
            GenerateSymbols();
        }
    }

    public void TurnOnPanel()
    {
        //lightningEffect

    }
}