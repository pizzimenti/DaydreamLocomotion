using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionTeleport : MonoBehaviour
{
    public float teleportRange = 50.0f;
    public GameObject teleportTarget;

    private LineRenderer laser;
    private Vector3 beamOrigin;
    private Vector3 beamDirection;

    private Vector3 targetPosition;
    private bool isTeleportable;


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

    // Actions to perform while user is selecting a destination
    private void TeleportPrep()
    {
        beamOrigin = GameObject.Find("Laser").transform.position;
        beamDirection = GameObject.Find("Laser").transform.forward;

        targetPosition = CalcDestination(beamOrigin, beamDirection, teleportRange);
        ShineLaser();
    }

    private void TeleportEngage()
    {
        laser.enabled = false;
        transform.position = targetPosition;
    }


    private Vector3 CalcDestination(Vector3 laserOrigin, Vector3 laserDirection, float range)
    {
        Ray ray = new Ray(laserOrigin, laserDirection);
        RaycastHit raycastHit;

        // Check to see if pointed at valid destination and in range
        if (Physics.Raycast(ray, out raycastHit, range))
        {
            GameObject targetObject = raycastHit.transform.gameObject;
            if (targetObject.tag == "Terrain") isTeleportable = true;
            else { isTeleportable = false; }
            return raycastHit.point;
        }
        else { return beamOrigin + (teleportRange * beamDirection); }
    }

    // Render laser beam
    private void ShineLaser()
    {
        if (!laser.enabled) laser.enabled = true;   // If line renderer is not already on, enable it
        if (isTeleportable)
        {
            laser.startColor = Color.green; // Set the laser color to green when it is striking a valid teleport destination
            laser.endColor = Color.green;
        }
        else
        {
            laser.startColor = Color.red; // Set the laser color to red when it is NOT striking a valid teleport destination
            laser.endColor = Color.red;
        }

        //targetPosition = beamOrigin + (teleportRange * beamDirection);
        laser.SetPosition(0, beamOrigin);
        laser.SetPosition(1, targetPosition);

    }
}
