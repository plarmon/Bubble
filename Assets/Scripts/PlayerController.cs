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
    private Rigidbody rb;
    // private PlayerInput playerInput;
    [SerializeField] private InputActionReference move;
    [SerializeField] private CinemachineFreeLook cam;

    // private Vector2 inputVector;
    private Vector2 movementVector;
    private Vector3 camForward, camRight, forwardRelative, rightRelative, movementDirection;
    private LayerMask groundLayer;
    private Transform camTransform;

    // Start is called before the first frame update
    void Start()
    {
        // playerInput = new PlayerInput();
        rb = GetComponent<Rigidbody>();
        groundLayer = LayerMask.GetMask("Ground");
        camTransform = Camera.main.transform;
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

        forwardRelative = movementVector.y * camForward;
        rightRelative = movementVector.x * camRight * lateralSpeedMult;

        movementDirection = forwardRelative + rightRelative;
    }

    private void FixedUpdate() {
        if (movementVector == Vector2.zero) return;

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1, groundLayer))
        {
            movementDirection -= Vector3.Project(movementDirection, hit.normal); //orthogonalize movementDirection and hit.normal  
        }
       
        if(rb.velocity.magnitude < maxSpeed) {
            rb.AddForce(movementDirection * speed);
        }
        
    }

    public float GetMaxSpeed() {
        return maxSpeed;
    }

    public Rigidbody GetRigidbody() {
        return rb;
    }
}
