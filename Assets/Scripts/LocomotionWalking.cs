using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionWalking : MonoBehaviour
{
    [Range(0f, 0.3f)]
    public float walkingSpeed = 0.15f; // speed of overall movement with 0.15 being close to walking speed
    [Range(0f, 1f)]
    public float strafeSpeed = 0.75f; // speed of side-to-side movement as a percentage of walkingSpeed

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GvrController.ClickButton) return;  // Prevents walking while user prepares to teleport
        if (GvrController.IsTouching)  Walk();
    }

    private void Walk()
    {
        Vector2 touchCoords = GvrController.TouchPos;   // gather user thumb position

        if (touchCoords.x > 0.6f || // don't transform if user thumb is relatively centered
            touchCoords.x < 0.4f ||
            touchCoords.y > 0.6f ||
            touchCoords.y < 0.4f )
        {
            transform.position += Camera.main.transform.forward * (-touchCoords.y + 0.5f) * walkingSpeed;
            transform.position += Camera.main.transform.right * (touchCoords.x - 0.5f) * walkingSpeed * strafeSpeed;
        }
    }
}