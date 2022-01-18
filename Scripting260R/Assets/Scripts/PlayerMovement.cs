using System;
using System.Collections;
using System.Collections.Generic;
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
    
    [Header("Drag")]
    public FloatOS playerDrag;
    [SerializeField] private float groundDrag;
    [SerializeField] private float airDrag = 1f;

    
    
    private float playerHeight = 2f;
    private float verticalMovement;
    private float HorizontalMovement;

    [Header("GroundDetection")]
    [SerializeField] private LayerMask groundMask;
    private bool grounded;
    private float groundDistance = 0.5f;

    private RaycastHit slopeHit;
    
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
        grounded = Physics.CheckSphere(transform.position - new Vector3(0,1,0), groundDistance, groundMask);
        print(grounded);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            Jump();
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
            
        PlayerInput();
        ControlDrag();
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

    private void Jump()
    {
        rigid.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}
