using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionTeleport : MonoBehaviour
{
    public float teleportRange = 50.0f;
    private LineRenderer laser;
    private Vector3 targetPosition;

    // Use this for initialization
    void Start()
    {
        laser = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GvrController.ClickButton)
            TeleportPrep();

        if (GvrController.ClickButtonUp)
            TeleportEngage();
    }

    // Shine a beam and calculate teleport destinations
    private void TeleportPrep()
    {
        Vector3 beamOrigin = GameObject.Find("Laser").transform.position;
        Vector3 beamDirection = GameObject.Find("Laser").transform.forward;

        //Vector3 beamDirection = GvrController.Orientation * Vector3.right;

        ShineLaser(beamOrigin, beamDirection, teleportRange);
    }

    private void TeleportEngage()
    {
        laser.enabled = false;
        transform.position = targetPosition;
    }

    private void ShineLaser(Vector3 laserOrigin, Vector3 laserDirection, float range)
    {
        /*  TODO: IMPLEMENT ONCE LASER IS RENDERING
        Ray ray = new Ray(laserOrigin, laserDirection);
        RaycastHit raycastHit;

        if(Physics.Raycast(ray, out raycastHit, range))
        {
            GameObject targetObject = raycastHit.transform.gameObject;
            if(targetObject.tag == "Terrain")
            {
                Debug.Log("Terrain struck with laser");
            }
        }
        */
        // Render laser beam
        if (!laser.enabled) laser.enabled = true;   // If line renderer is not already on, enable it
        targetPosition = laserOrigin + (range * laserDirection);
        laser.SetPosition(0, laserOrigin);
        laser.SetPosition(1, targetPosition);

    }
}
