using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionWalking : MonoBehaviour
{
    public float walkingSpeed = 0.5f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GvrController.ClickButton)
        {
            Walk();
        }
    }

    private void Walk()
    {
        Vector3 velocity = new Vector3(0f, 0f, walkingSpeed);
        transform.position += Camera.main.transform.forward;
    }
}
