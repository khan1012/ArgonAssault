using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up/down or right/left based on player input")]
    [SerializeField] float controlSpeed = 50f;
    [Tooltip("How far ship moves right/left on screen")]
    [SerializeField] float xRange = 20f;
    [Tooltip("How far ship moves up/down on screen")]
    [SerializeField] float yRange = 15f;
    float xThrow;
    float yThrow;

    [Header("Screen Position based Tuning")]
    [SerializeField] float positionPitchFactor = -1.5f;
    [SerializeField] float positionYawFactor = 0.5f;

    [Header("Player Input based Tuning")]
    [SerializeField] float controlPitchFactor = -7.5f;
    [SerializeField] float controlRollFactor = -2f;

    [Header("Laser Gun Array")]
    [SerializeField] GameObject[] lasers;

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
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
