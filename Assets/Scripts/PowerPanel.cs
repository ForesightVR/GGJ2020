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
    public MeshRenderer buttonRenderer;
    public PowerPanelButton panelButton;

    public TextMeshProUGUI[] symbolAreas;
    public List<string> generatedSymbols = new List<string>();
    public List<GameObject> buttons = new List<GameObject>();
    public Animator[] pistons;

    [Space]
    public AudioClip pistonOpen;
    public AudioClip pistonClose;

    Animator animator;
    PowerManager powerManager;
    PowerPanelButton powerBtn;
    Coroutine turnOffCoroutine;

    int symbolIndex;
    char[] possibleSymbols;
    bool complete;

    private void Awake()
    {
        foreach (TextMeshProUGUI symbolArea in symbolAreas)
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

        foreach (TextMeshProUGUI symbolArea in symbolAreas)
            symbolArea.text = "";

        for (int i = 0; i < symbolAmt; i++)
        {
            generatedSymbols.Add(possibleSymbols[i].ToString()); //UnityEngine.Random.Range(0, possibleSymbols.Length - 1)
            symbolAreas[i].text = generatedSymbols[i];
        }
        foreach (TextMeshProUGUI symbolArea in symbolAreas)
            symbolArea.gameObject.SetActive(true);

        foreach (Animator anim in pistons)
        {
            anim.GetComponent<AudioSource>().PlayOneShot(pistonOpen);
            anim.SetTrigger("OpenPiston");
        }
    }

    void MatchButtonsToSymbols()
    {
        int index = 0;
        buttons.Shuffle();

        foreach (GameObject go in buttons)
        {
            go.GetComponentInChildren<PanelButton>().Init(this, generatedSymbols[index].ToString());
            index++;
        }
    }
    public void TurnOnPanel()
    {
        if (complete) return;
        foreach (Animator anim in pistons)
        {
            anim.GetComponent<AudioSource>().PlayOneShot(pistonClose);
            anim.SetTrigger("ClosePiston");
        }
        //lightningEffect
        powerBtn.GetComponent<Collider>().enabled = false;
        animator.SetTrigger("OpenPowerPanel");

        foreach (TextMeshProUGUI symbolArea in symbolAreas)
            symbolArea.gameObject.SetActive(false);

        foreach (GameObject button in buttons)
        {
            button.GetComponentInChildren<Collider>().enabled = true;
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

        if (generatedSymbols[symbolIndex] == symbol)
        {
            if (symbolIndex < generatedSymbols.Count - 1)
            {
                symbolIndex++;
            }
            else
            {
                StopCoroutine(turnOffCoroutine);

                animator.SetTrigger("ClosePowerPanel");
                buttonRenderer.material.color = Color.black;
                powerManager.EndPanel();
                complete = true;
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
        animator.SetTrigger("ClosePowerPanel");

        foreach (GameObject button in buttons)
        {
            button.GetComponentInChildren<Collider>().enabled = false;
        }
    }
}

public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        System.Random rnd = new System.Random();
        for (var i = 0; i < list.Count; i++)
            list.Swap(i, rnd.Next(i, list.Count));
    }

    public static void Swap<T>(this IList<T> list, int i, int j)
    {
        var temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }
}

