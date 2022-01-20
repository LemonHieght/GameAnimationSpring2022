using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform Orientation;

    [Header("Movement")]
    public FloatOS playerSpeed;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float movementMultiplier = 10f;
    [SerializeField] private float airMovementMultiplier = 0.4f;
    

    [Header("Jump")]
    [SerializeField] private float jumpForce = 5f;
    private bool wallBack;
    private bool wallFront;
    [SerializeField] private float wallDistance = 1.5f;
    
    [Header("Drag")]
    public FloatOS playerDrag;
    [SerializeField] private float groundDrag;
    [SerializeField] private float airDrag = 1f;

    [Header("Sprinting")]
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float SprintSpeed = 6f;
    [SerializeField] private float acceleration = 10f;
    
    [Header("GroundDetection")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;
    private bool grounded;
    [SerializeField] private float groundDistance = 0.5f;

    private float playerHeight = 2f;
    private float verticalMovement;
    private float HorizontalMovement;

    public RaycastHit slopeHit;
    
    private Vector3 moveDirection;
    private Vector3 slopeMoveDirection;
    
    private Rigidbody rigid;

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.freezeRotation = true;
        moveSpeed = playerSpeed.value;
        groundDrag = playerDrag.value;
        
        
    }

    private void Update()
    {
        PlayerInput();
        ControlSpeed();
        ControlDrag();
        WallCheck();
        
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if (Input.GetButtonDown("Jump") && grounded)
        {
            if (!wallBack || !wallFront)
            {
                Debug.Log(" no wall");
            }
            else
            {
                Debug.Log("wall");
            }
            Jump();
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }
    
    private void FixedUpdate()
    {
        movePlayer();
    }
    

    private void PlayerInput()
    {
        HorizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = Orientation.transform.forward * verticalMovement + Orientation.transform.right * HorizontalMovement;
    }

    private void movePlayer()
    {
        
        if (grounded && !OnSlope())
        {
            rigid.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (grounded && OnSlope())
        {
            rigid.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if(!grounded)
        {
            rigid.AddForce(moveDirection.normalized * moveSpeed * airMovementMultiplier, ForceMode.Acceleration);
        }
    }
    
    private void Jump()
    {
        rigid.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        rigid.velocity = new Vector3(rigid.velocity.x, 0, rigid.velocity.z);
    }

    
    private void ControlDrag()
    {
        if (grounded)
        {
            rigid.drag = groundDrag;
        }
        else
        {
            rigid.drag = airDrag;
        }
    }

    void ControlSpeed()
    {
        if (Input.GetButton("Fire3") && grounded)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, SprintSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }

    private void WallCheck()
    {
        wallBack = Physics.Raycast(transform.position, -Orientation.forward, wallDistance);
        wallFront = Physics.Raycast(transform.position, Orientation.forward, wallDistance);
    }
}
