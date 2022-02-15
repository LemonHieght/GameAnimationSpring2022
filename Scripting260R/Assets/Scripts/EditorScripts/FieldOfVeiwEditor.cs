using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FeildOfView))]
public class FieldOfVeiwEditor : Editor
{
    void OnSceneGUI()
    {
        FeildOfView fow = (FeildOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
        
        Vector3 viewAngleA = fow.AngleDirection(-fow.viewAngle / 2, false);
        Vector3 viewAngleB = fow.AngleDirection(fow.viewAngle / 2, false);
        
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

        Handles.color = Color.red;
        foreach (Transform visableTarget in fow.visibleTargets)
        {
            Handles.DrawLine(fow.transform.position, visableTarget.position);
        }
    }
}
