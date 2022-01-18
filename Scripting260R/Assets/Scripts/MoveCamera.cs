using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform camera;
    
    void Update()
    {
        transform.position = camera.position;
    }
}
