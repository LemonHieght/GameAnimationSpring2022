using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerLook : MonoBehaviour
{
    private WallRun wallRun;
    
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;
    
    [SerializeField] private float multiplier = 0.01f;
    
    [SerializeField] private Transform cam;
    
    [SerializeField] private Transform orientation;

    private float lookX;
    private float lookY;
    
    private float rotationX;
    private float rotationY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        wallRun = GetComponent<WallRun>();
    }

    private void Update()
    {
        PlayerInput();
        
        cam.transform.localRotation = Quaternion.Euler(rotationX, rotationY, wallRun.tilt);
        orientation.transform.rotation = Quaternion.Euler(0, rotationY, 0);
    }

    void PlayerInput()
    {
        lookX = Input.GetAxisRaw("Mouse X");
        lookY = Input.GetAxisRaw("Mouse Y");

        rotationY += lookX * sensX * multiplier;
        rotationX -= lookY * sensY * multiplier;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
    }
}
