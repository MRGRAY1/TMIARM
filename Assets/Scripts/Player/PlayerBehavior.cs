using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Summary Of File
/// </summary>

// ToDo:
// - Add specific logic or initialization here
public class PlayerBehavior : MonoBehaviour
{
    #region Variables
    // Declare fields, constants, and serialized variables here.
    [Header("References")]
    [SerializeField]
    private InputManager playerInput;
    [SerializeField]
    private float moveSpeed = 7.0f;
    [SerializeField]
    private float rotateSpeed = 10.0f;
    private Transform playerTransform;
    private bool isWalking = false;

    [SerializeField] private float acceleration = 12f;
    [SerializeField] private float deceleration = 16f;

    private Vector3 currentVelocity;



    #endregion

    #region Functions
    // Called before Start().
    // Initialize references and set up components.
    private void Awake()
    {
        playerTransform = transform;
    }

    // Called before the first frame update.
    // Run startup logic that depends on other objects being initialized.
    private void Start()
    {

    }

    // Called each time the object becomes enabled and active.
    // Subscribe to events or reset state here.
    private void OnEnable()
    {

    }

    // Called when the object is disabled or destroyed.
    // Unsubscribe from events or clean up.
    private void OnDisable()
    {

    }

    // Called once per frame.
    // Handle per-frame logic such as input or movement.
    private void Update()
    {

    }

    public bool IsWalking()
    {
        return isWalking;
    }
    private void FixedUpdate()
    {
        Vector2 inputVector = playerInput.GetMovementVectorNormalized();

        Vector3 forward = playerTransform.forward;
        Vector3 right = playerTransform.right;

        Vector3 targetDirection = (forward * inputVector.y + right * inputVector.x);
        targetDirection.y = 0f;

        Vector3 targetVelocity = targetDirection.normalized * moveSpeed;

        float accel = (targetDirection.magnitude > 0.01f) ? acceleration : deceleration;

        currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, accel * Time.fixedDeltaTime);

        playerTransform.position += currentVelocity * Time.fixedDeltaTime;

        isWalking = currentVelocity.magnitude > 0.1f;
    }


    #endregion
}