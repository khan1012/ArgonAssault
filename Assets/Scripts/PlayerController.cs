using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float controlSpeed = 50f;
    [SerializeField] float xRange = 20f;
    [SerializeField] float yRange = 15f;

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    void ProcessRotation()
    {
        float pitch = 0f;
        float yaw = 0f;
        float roll = 0f;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessTranslation()
    {
        float xThrow = Input.GetAxis("Horizontal");
        float yThrow = Input.GetAxis("Vertical");

        float xOffest = xThrow * Time.deltaTime * controlSpeed;
        float newXPosition = transform.localPosition.x + xOffest;
        float clampedXPosition = Mathf.Clamp(newXPosition, -xRange, xRange);

        float yOffest = yThrow * Time.deltaTime * controlSpeed;
        float newYPosition = transform.localPosition.y + yOffest;
        float clampedYPosition = Mathf.Clamp(newYPosition, -yRange, yRange);

        transform.localPosition = new Vector3(
            clampedXPosition,
            clampedYPosition,
            transform.localPosition.z
        );
    }
}
