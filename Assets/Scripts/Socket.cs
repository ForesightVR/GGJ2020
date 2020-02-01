using OuterRimStudios.Utilities;
using TMPro;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Socket : MonoBehaviour
{
    public Panel panel;
    public TextMeshProUGUI text;
    private char character;

    public void Initialize(char character)
    { 
        this.character = character;
        text.SetText(character.ToString());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("VacuumTube"))
        {
            return;
        }
     
        var otherObject = other.gameObject;
        var otherComponentParent = otherObject.GetComponentInParent<VacuumTube>();
        
        if (character == otherComponentParent.character)
        {
            var parent = otherObject.transform.parent;
            parent.SetParent(transform);
            
            // Disable collision/throwable/gravity
            other.enabled = false;
            otherComponentParent.GetComponent<Throwable>().enabled = false;

            var rigidBody = otherComponentParent.GetComponent<Rigidbody>();
            rigidBody.useGravity = false;
            rigidBody.isKinematic = false;
            
            parent.localPosition = new Vector3(0, 0.12f, 0);
            parent.localRotation = Quaternion.Euler(0, 90, 0);

            panel.newLockedSocket();
        }
    }
}
