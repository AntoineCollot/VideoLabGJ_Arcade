using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody carRigidbody;

    [HideInInspector] public Vector2 inputs;
    bool isGrounded;
    public Transform groundRaycastOrigin;
    public float groundRayLength;
    public LayerMask groundLayers;
    bool isBreaking = false;

    [Header("Car Settings")]
    public float acceleration=8;
    public float backwardAcceleration=4;
    public float maxSpeed=50;
    public float turnStrength=180;
    public float gravity = -10;
    public float dragOnGround = 3;

    Vector3 originalPosition;
    Quaternion originalRotation;

    public ParticleSystem smokeRight;
    public ParticleSystem smokeLeft;
    float emissionAmount;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        RacingManager.Instance.onMiniGameOver.AddListener(OnMiniGameOver);
        emissionAmount = smokeLeft.emission.rateOverTimeMultiplier;
    }

    private void Update()
    {
        if (!RacingManager.Instance.gameIsPlaying)
            return;

        inputs = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        isBreaking = Input.GetButton("Action");
        ParticleSystem.EmissionModule emissionRight = smokeRight.emission;
        ParticleSystem.EmissionModule emissionLeft = smokeLeft.emission;
        if(isBreaking)
        {
            emissionRight.rateOverTimeMultiplier = emissionAmount;
            emissionLeft.rateOverTimeMultiplier = emissionAmount;
        }
        else
        {
            emissionRight.rateOverTimeMultiplier = 0;
            emissionLeft.rateOverTimeMultiplier = 0;
        }

        if (isGrounded)
        {
            float movementSign = Mathf.Sign(Vector3.Dot(carRigidbody.velocity, transform.forward));
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, inputs.x * turnStrength * Time.deltaTime * Mathf.Abs(inputs.y)* movementSign, 0));
        }

        transform.position = carRigidbody.position;
    }

    private void FixedUpdate()
    {
        if (!RacingManager.Instance.gameIsPlaying || !RacingManager.Instance.RaceIsOn)
            return;

        isGrounded = false;
        RaycastHit hit;
        Ray ray = new Ray(groundRaycastOrigin.position, -transform.up);
        Debug.DrawRay(ray.origin, ray.direction* groundRayLength, Color.red);
        if(Physics.Raycast(ray, out hit, groundRayLength, groundLayers))
        {
            isGrounded = true;
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        if (isGrounded)
        {
            if (inputs.y >= 0 && !isBreaking)
                carRigidbody.AddForce(transform.forward * acceleration * 1000f * inputs.y, ForceMode.Force);
            else
                carRigidbody.AddForce(transform.forward * backwardAcceleration * 1000f * inputs.y, ForceMode.Force);
        }

        if (carRigidbody.velocity.magnitude > maxSpeed)
            carRigidbody.velocity = carRigidbody.velocity.normalized * maxSpeed;

        carRigidbody.AddForce(Vector3.up * gravity, ForceMode.Force);

        if (!isGrounded)
            carRigidbody.drag = 0.5f;
        else if(isBreaking)
            carRigidbody.drag = dragOnGround*0.5f;
        else
            carRigidbody.drag = dragOnGround;
    }

    void OnMiniGameOver()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        carRigidbody.transform.position = originalPosition;
        carRigidbody.transform.rotation = originalRotation;
        carRigidbody.velocity = Vector3.zero;
    }
}
