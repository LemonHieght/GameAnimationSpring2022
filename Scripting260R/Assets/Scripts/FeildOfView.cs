using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeildOfView : MonoBehaviour
{
    public Vector3OS playerLocation;
    
    [SerializeField] private LayerMask player;
    [SerializeField] private LayerMask enviorment;
    [SerializeField] private Transform enemySight;
    public List<Transform> visibleTargets = new List<Transform>();
    
    public float viewRadius = 13f;
    public float viewAngle;

    public Vector3 AngleDirection(float angleDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleDegrees += transform.eulerAngles.y;
        }
        
        return new Vector3(Mathf.Sin(angleDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleDegrees * Mathf.Deg2Rad));
    }

    private void Start()
    {
        StartCoroutine("FindTargetWithDelay", 0.2f);
    }

    void FindVisableTarget()
    {
        visibleTargets.Clear();
        
        Collider[] targetsInView = Physics.OverlapSphere(transform.position, viewRadius, player);
        for (int i = 0; i < targetsInView.Length; i++)
        {
            Transform target = targetsInView[i].transform;
            Vector3 targetDirection = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, targetDirection) < viewAngle / 2)
            {
                float targetDistance = Vector3.Distance(transform.position, target.position);
                
                if (!Physics.Raycast(transform.position, targetDirection, targetDistance, enviorment))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }

    IEnumerator FindTargetWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisableTarget();
        }
    }
}
