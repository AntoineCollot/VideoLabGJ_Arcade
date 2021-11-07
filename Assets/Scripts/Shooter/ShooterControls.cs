using System;
using UnityEngine;

public class ShooterControls : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    CharacterController controller;
    Vector3 originalPosition;

    public float gravity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        originalPosition = transform.position;
        ShooterManager.Instance.onMiniGameOver.AddListener(OnMiniGameOver);
    }

    private void OnMiniGameOver()
    {
        transform.position = originalPosition;
    }

    void Update()
    {
        if (!ShooterManager.Instance.gameIsPlaying)
            return;

        Vector3 inputMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        inputMovement *= _movementSpeed * Time.deltaTime;

        Vector3 forward = transform.forward;
        forward.y = 0;
        forward.Normalize();
        Vector3 right = transform.right;
        right.y = 0;
        right.Normalize();

        Vector3 effectiveMovement = inputMovement.z * forward + inputMovement.x * right+Vector3.down * gravity * Time.deltaTime;

        controller.Move(effectiveMovement);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.GetComponent<ShooterEnemyAI>() != null)
            ShooterManager.Instance.PlayerDamaged();
    }
}