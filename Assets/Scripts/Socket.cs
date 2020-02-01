using TMPro;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Socket : MonoBehaviour
{
    public Panel panel;
    public TextMeshProUGUI text;
    public GameObject socketedVacuumTubes;

    private char _character;
    public char character
    {
        get => _character;
        set
        {
            _character = value;
            text.SetText(value.ToString());
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.CompareTag("VacuumTube"))
        {
            return;
        }
     
        var vacuumTubeCollider = collider.gameObject;
        var vacuumTubeScript = vacuumTubeCollider.GetComponentInParent<VacuumTube>();
        
        if (character == vacuumTubeScript.character)
        {
            collider.enabled = false;
            vacuumTubeScript.GetComponent<Throwable>().enabled = false;

            var rigidBody = vacuumTubeScript.GetComponent<Rigidbody>();
            rigidBody.useGravity = false;
            rigidBody.constraints = RigidbodyConstraints.FreezeAll;
         
            var vacuumTube = vacuumTubeCollider.transform.parent;

            // Temporarily set the parent to the socket so positioning is simple
            vacuumTube.SetParent(transform);
            vacuumTube.localPosition = new Vector3(0, 0.12f, 0);
            vacuumTube.localRotation = Quaternion.Euler(0, -90, 0);

            vacuumTube.SetParent(socketedVacuumTubes.transform);

            // Let the panel know we've locked a tube to a socket
            panel.NewLockedSocket();
        }
    }
}
