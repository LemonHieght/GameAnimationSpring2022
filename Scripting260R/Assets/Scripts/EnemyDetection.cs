using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private bool seePlayer;
    [SerializeField] private LayerMask player;
    [SerializeField] private Transform enemyDetection;
    [SerializeField] private float viewDistance = 4f;

    // Update is called once per frame
    void Update()
    {
        if (seePlayer)
        {
            Debug.Log("Player Seen");
        }
        else
        {
            Debug.Log("No Player");
        }
    }

    private void FixedUpdate()
    {
        ViewDetection();
    }

    void ViewDetection()
    {
        seePlayer = Physics.CheckSphere(enemyDetection.position, viewDistance, player);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(enemyDetection.position, viewDistance);
    }
}
