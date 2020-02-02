using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour
{
    public LayerMask interactionLayer;
    LineRenderer line;

    Reflector reflector;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    public void Reflect(Vector3 direction, Vector3 hitNormal, Vector3 hitPoint)
    {
        if (line.enabled == false)
            line.enabled = true;

        Vector3 reflected = Vector3.Reflect(direction, hitNormal);

        line.SetPosition(0, hitPoint);
        line.SetPosition(1, reflected * 1000);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, reflected, out hit, 1000, interactionLayer))
        {
            if (hit.transform.tag.Equals("Reflector"))
            {
                reflector = hit.transform.GetComponentInChildren<Reflector>();
                reflector.Reflect(transform.forward, hit.normal, hit.point);
            }
            else if (reflector)
            {
                reflector.DisableReflection();
                reflector = null;
            }
        }
    }

    public void DisableReflection()
    {
        line.enabled = false;
    }
}
