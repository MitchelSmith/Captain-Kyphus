using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("Meters Per Second")] [SerializeField] float controlSpeed = 15f;
    [Tooltip("Meters")] [SerializeField] float xRange = 8f;
    [Tooltip("Meters")] [SerializeField] float yRange = 7f;
    [SerializeField] GameObject[] guns;

    [Header("Screen-position Parameters")]
    [SerializeField] float positionPitchFactor = -3f;
    [SerializeField] float positionYawFactor = 4f;

    [Header("Control-throw Parameters")]
    [SerializeField] float controlRollFactor = -10f;
    [SerializeField] float controlPitchFactor = -10f;

    float xThrow, yThrow;
    bool controlEnabled = true;

    // Update is called once per frame
    void Update()
    {
        if (controlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    private void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float yOffset = yThrow * controlSpeed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;
        
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    /// <summary>
    /// Called by string reference
    /// </summary>
    private void HandleDeathSequence()
    {
        controlEnabled = false;
    }

    private void ProcessFiring()
    {
        if (Input.GetButton("Fire"))
        {
            ActivateGuns();
        }
        else
        {
            DeactivateGuns();
        }
    }

    private void ActivateGuns()
    {
        foreach (GameObject gun in guns)
        {
            var emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = true;
        }
    }

    private void DeactivateGuns()
    {
        foreach (GameObject gun in guns)
        {
            var emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = false;
        }
    }
}
