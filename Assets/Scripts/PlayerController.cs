using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float controlSpeed = 50f;
    [SerializeField] float xRange = 20f;
    [SerializeField] float yRange = 15f;
    float xThrow;
    float yThrow;
    [SerializeField] float positionPitchFactor = -1.5f;
    [SerializeField] float controlPitchFactor = -7.5f;
    [SerializeField] float positionYawFactor = 0.5f;
    [SerializeField] float controlRollFactor = 2f;

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yawDueToPosition = transform.localPosition.x * positionYawFactor;
        float yaw = yawDueToPosition;

        float rollDueToControl = transform.localPosition.x * controlRollFactor;
        float roll = rollDueToControl;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

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
