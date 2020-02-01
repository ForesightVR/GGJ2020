using OuterRimStudios.Utilities;
using TMPro;
using UnityEngine;

public class Socket : MonoBehaviour
{
    public Panel panel;
    public TextMeshProUGUI text;
    private char character;

    public void Initialize(char character)
    { 
        this.character = character;
    }

    void Start()
    {
        character = panel.possibleCharacters.ToCharArray().GetRandomItem();
        text.SetText(character.ToString());
    }
}
