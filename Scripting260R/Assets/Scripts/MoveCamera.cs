using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Plai Rigidbody FPS Controller
public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform camera;
    
    void Update()
    {
        transform.position = camera.position;
    }
}
