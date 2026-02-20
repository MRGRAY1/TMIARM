using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Sensitivity")]
    [SerializeField, Tooltip("Mouse X sensitivity (yaw)")]
    private float xSensitivity = 1.5f;
    [SerializeField, Tooltip("Mouse Y sensitivity (pitch)")]
    private float ySensitivity = 1.5f;

    [Header("References")]
    [SerializeField, Tooltip("Transform of the camera for pitch rotation")]
    private Transform cameraTransform;

    [Header("Smoothing")]
    //[SerializeField, Tooltip("Enable smooth camera movement")]
    //private bool enableSmoothing = true;
    [SerializeField, Tooltip("Speed at which smoothing interpolates towards target rotation. Higher = snappier")]
    private float smoothSpeed = 10f;


    private PlayerInputSystem playerInputs;
    private Vector2 mouseDelta;
    private Vector2 targetRotation;
    private Vector2 currentRotation;

    [SerializeField] private float mouseSmoothing = 0.08f;
    private Vector2 smoothMouseDelta;


    /// <summary>
    /// Initialize input callbacks
    /// </summary>
    private void Awake()
    {
        playerInputs = new PlayerInputSystem();

        // Update mouse delta when Look input is performed
        playerInputs.Main.Look.performed += ctx => mouseDelta = ctx.ReadValue<Vector2>();

        // Reset mouse delta to zero when input stops
        playerInputs.Main.Look.canceled += _ => mouseDelta = Vector2.zero;
    }

    /// <summary>
    /// Enable input and lock/hide the cursor
    /// </summary>
    private void OnEnable()
    {
        playerInputs.Enable();
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor to center
        Cursor.visible = false;                    // Hide cursor for FPS feel
    }

    private void OnDisable()
    {
        playerInputs.Disable();
    }

    private void LateUpdate()
    {
        // Smooth mouse delta itself
        smoothMouseDelta = Vector2.Lerp(smoothMouseDelta, mouseDelta, mouseSmoothing);

        targetRotation.x -= smoothMouseDelta.y * ySensitivity;
        targetRotation.y += smoothMouseDelta.x * xSensitivity;

        targetRotation.x = Mathf.Clamp(targetRotation.x, -90f, 90f);

        currentRotation = Vector2.Lerp(currentRotation, targetRotation, smoothSpeed * Time.deltaTime);

        cameraTransform.localRotation = Quaternion.Euler(currentRotation.x, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, currentRotation.y, 0f);
    }

}