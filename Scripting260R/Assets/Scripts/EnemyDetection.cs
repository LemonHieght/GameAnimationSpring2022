using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public Vector3OS playerLocation;
    
    private bool seePlayer;
    [SerializeField] private LayerMask player;
    [SerializeField] private LayerMask enviorment;
    [SerializeField] private Transform enemySight;
    private Vector3 target;
    [SerializeField] private float viewRadius = 13f;
    [SerializeField] private float viewAngle;

    // Update is called once per frame
    
    private void FixedUpdate()
    {
        ViewDetection();
        target = playerLocation.value;
    }

    void ViewDetection()
    {
        
    }
}
