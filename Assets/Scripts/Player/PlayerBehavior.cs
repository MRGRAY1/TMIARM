using System;
using System.Collections.Generic;
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
    [Header("References")] [SerializeField]
    private float moveSpeed = 7.0f;

    //[SerializeField]
    //private float rotateSpeed = 10.0f;
    private Transform playerTransform;
    private bool isWalking = false;
    [SerializeField] private GameObject _playCamera;
    public float camRayCastDist = 3f;

    [SerializeField] private float acceleration = 12f;
    [SerializeField] private float deceleration = 16f;

    private Vector3 currentVelocity;

    [Header("Jumping")] [SerializeField] private float groundRaycast_Dist = .5f;

    [SerializeField, Tooltip("Layer for Ground")]
    private LayerMask groundLayerMask;

    [SerializeField] private bool is_grounded;

    [SerializeField, Tooltip("Jump Force")]
    private float jump_Force = 5f;

    private Rigidbody rb;

    #endregion

    #region Functions

    // Called before Start().
    // Initialize references and set up components.
    private void Awake()
    {
        playerTransform = transform;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // prevents tipping over
        GameEvents.PlayerJump += PlayerJump;
        GameEvents.PlayerInteract += InteractWithObject;
    }

    private void PlayerJump(object sender)
    {
        if (this.is_grounded)
        {
            this.rb.AddForce(Vector3.up * this.jump_Force, ForceMode.Impulse);
        }
    }

    private void InteractWithObject(object sender)
    {
        RaycastHit hit;
        Logger.Log("Key Pressed");
        if (Physics.Raycast(_playCamera.transform.position, _playCamera.transform.forward, out hit, camRayCastDist))
        {
            Logger.Log("Collider Hit");
            if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
            {
                Logger.Log("Object Hit");
                interactable.Interact();
            }

            if (hit.collider.TryGetComponent<NpcInteractions>(out var npc))
            {
                Logger.Log("NPC Hit");
                npc.Interact();
            }
        }
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
        GameEvents.PlayerJump -= PlayerJump;
        GameEvents.PlayerInteract -= InteractWithObject;
    }

    // Called once per frame.
    // Handle per-frame logic such as input or movement.
    private void Update()
    {
        Debug.DrawRay(_playCamera.transform.position, _playCamera.transform.forward * camRayCastDist, Color.red);
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = Managers.Instance.InputManager.GetMovementVectorNormalized();

        Vector3 forward = playerTransform.forward;
        Vector3 right = playerTransform.right;

        Vector3 moveDir = (forward * inputVector.y + right * inputVector.x);
        moveDir.y = 0f;

        Vector3 targetVelocity = moveDir.normalized * moveSpeed;

        float accelRate = (moveDir.magnitude > 0.01f) ? acceleration : deceleration;

        currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, accelRate * Time.fixedDeltaTime);

        rb.MovePosition(rb.position + currentVelocity * Time.fixedDeltaTime);

        isWalking = currentVelocity.magnitude > 0.1f;


        Vector3 raycastSpawn = new Vector3(transform.position.x, transform.position.y + .25f, transform.position.z);
        RaycastHit ground_hit;
        Debug.DrawRay(raycastSpawn, Vector3.down * this.groundRaycast_Dist, Color.green);

        if (Physics.Raycast(raycastSpawn, Vector3.down, out ground_hit, this.groundRaycast_Dist, groundLayerMask))
        {
            this.is_grounded = true;
        }
        else
        {
            this.is_grounded = false;
        }
    }

    #endregion
}