using TMPro;
using UnityEngine;

public class VacuumTube : MonoBehaviour
{
    public TextMeshProUGUI text;
    public char character;
    
    public void Initialize(char character)
    {
        this.character = character;
        text.SetText(character.ToString());
    }
}
