using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    [SerializeField] private float lateralSpeedMult = 2;
    [SerializeField] private float maxSpeed = 15f; 
    [SerializeField] private float jumpForce = 7;
    private Rigidbody rb;
    // private PlayerInput playerInput;
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference jump;
    [SerializeField] private CinemachineFreeLook cam;
    [SerializeField] private GameObject shadow;
    private Vector3 shadowPoint;
    [SerializeField] private GameObject playerModel;
    private Vector3 playerPoint;

    // private Vector2 inputVector;
    private Vector2 movementVector;
    private Vector3 camForward, camRight, forwardRelative, rightRelative, movementDirection;
    private LayerMask groundLayer;
    private Transform camTransform;

    public bool isGrounded = false;
    private bool isJumping = false;
    private float jumpTime;

    // Start is called before the first frame update
    void Start()
    {
        // playerInput = new PlayerInput();
        rb = GetComponent<Rigidbody>();
        groundLayer = LayerMask.GetMask("Ground");
        camTransform = Camera.main.transform;

        shadowPoint = transform.position - shadow.transform.position;
        playerPoint = transform.position - playerModel.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Getting Inputs
        movementVector = move.action.ReadValue<Vector2>();
        camForward = camTransform.forward;
        camRight = camTransform.right;

        camForward.y = 0;
        camRight.y = 0;

        shadow.transform.position = transform.position + shadowPoint;
        if(playerModel) {
            playerModel.transform.position = transform.position - playerPoint;
        }

        if(jump.action.WasPressedThisFrame() && GetIsGrounded()) {
            // if(isJumping) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            // }
        }

        if(playerModel) {
            playerModel.transform.forward = Vector3.Lerp(playerModel.transform.forward, forwardRelative, 0.25f);
        }

        if(movementVector.Equals(Vector2.zero)) return;

        forwardRelative = movementVector.y * camForward;
        rightRelative = movementVector.x * camRight * lateralSpeedMult;

        movementDirection = forwardRelative + rightRelative;
    }

    private void FixedUpdate() {
        if (movementVector.Equals(Vector2.zero)) return;

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1, groundLayer))
        {
            Debug.DrawRay(transform.position, Vector3.Project(movementDirection, hit.normal), Color.red);
            movementDirection -= Vector3.Project(movementDirection, hit.normal); //orthogonalize movementDirection and hit.normal  
        }
       
        if(rb.velocity.magnitude < maxSpeed) {
            rb.AddForce(movementDirection * speed);
        }
    }

    public bool GetIsGrounded() {
        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1, groundLayer)) {
            // Debug.Log("Is Grounded");
            isGrounded = true;
            return true;
        } else {
            // Debug.Log("Isn't Grounded");
            isGrounded = false;
            return false;
        }
    }

    public float GetMaxSpeed() {
        return maxSpeed;
    }

    public Rigidbody GetRigidbody() {
        return rb;
    }
}
