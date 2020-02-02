using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LayerMask interactionLayer;
    public float updateFrequnecy;
    public int maxReflections = 3;
    LineRenderer line;
    Reflector reflector;
    Crystal crystal;
    Ring ring;

    float timer;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        StartCoroutine(DrawLaser());
    }

    private void Update()
    {
        if(timer  >= updateFrequnecy)
        {
            timer = 0;
            StartCoroutine(DrawLaser());
        }

        timer += Time.deltaTime;
    }

    IEnumerator DrawLaser()
    {
        line.SetPosition(0, transform.position);

        int vertexCounter = 1;
        int laserReflected = 1;
        bool isStillDrawing = true;
        bool isHittingCrystal = false;
        bool isHittingRing = false;

        Vector3 laserDirection = transform.forward;
        Vector3 lastLaserPosition = transform.position;

        while (isStillDrawing)
        {
            if (Physics.Raycast(lastLaserPosition, laserDirection, out RaycastHit hit, 1000f, interactionLayer))
            {
                if (hit.transform.tag.Equals("Reflector"))
                {
                    laserReflected++;
                    vertexCounter += 3;
                    line.positionCount = vertexCounter;
                    line.SetPosition(vertexCounter - 3, Vector3.MoveTowards(hit.point, lastLaserPosition, .01f));
                    line.SetPosition(vertexCounter - 2, hit.point);
                    line.SetPosition(vertexCounter - 1, hit.point);

                    lastLaserPosition = hit.point;
                    laserDirection = Vector3.Reflect(laserDirection, hit.normal);
                }
                else if(hit.transform.tag.Equals("Blocker"))
                {
                    laserReflected++;
                    vertexCounter++;
                    line.positionCount = vertexCounter;
                    line.SetPosition(vertexCounter - 1, hit.point);

                    isStillDrawing = false;
                    isHittingCrystal = false;
                    isHittingRing = false;
                }
                else if (hit.transform.tag.Equals("Ring"))
                {
                    laserReflected++;
                    vertexCounter++;
                    line.positionCount = vertexCounter;
                    line.SetPosition(vertexCounter - 1, hit.point);

                    Ring thisRing = hit.transform.gameObject.GetComponent<Ring>();

                    if (ring && ring != thisRing)
                        ring.StopOverheat();

                    ring = thisRing;
                    ring.StartOverheat();

                    isStillDrawing = false;
                    isHittingCrystal = false;
                    isHittingRing = true;
                }
                else if (hit.transform.tag.Equals("Crystal"))
                {
                    laserReflected++;
                    vertexCounter++;
                    line.positionCount = vertexCounter;
                    line.SetPosition(vertexCounter - 1, hit.point);

                    if (!crystal)
                    {
                        crystal = hit.transform.gameObject.GetComponent<Crystal>();
                        crystal.StartOverheat();
                    }
                    isStillDrawing = false;
                    isHittingCrystal = true;
                    isHittingRing = false;
                }
                else
                {
                    laserReflected++;
                    vertexCounter++;
                    line.positionCount = vertexCounter;
                    line.SetPosition(vertexCounter - 1, lastLaserPosition + laserDirection.normalized * 1000f);
                    isStillDrawing = false;
                }
            }
            else
            {
                isStillDrawing = false;                
            }

            if (laserReflected > maxReflections)
                isStillDrawing = false;    
        }

        if (!isHittingCrystal)
        {
            if (crystal)
            {
                crystal.StopOverheat();
                crystal = null;
            }
        }

        if (!isHittingRing)
        {
            if (ring)
            {
                ring.StopOverheat();
                ring = null;
            }
        }

        yield return new WaitForEndOfFrame();
        
    }



            ////Talk to mirror

            //if (hit.transform.tag.Equals("Reflector"))
            //{
            //    reflector = hit.transform.GetComponentInChildren<Reflector>();
            //    reflector.Reflect(transform.forward, hit.normal, hit.point);
            //}
            //else if(reflector)
            //{
            //    reflector.DisableReflection();
            //    reflector = null;
            //}





 }
