using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
        [SerializeField] private Transform orientation;
        
        [Header("Wall Detection")]
        [SerializeField] private float wallDistance = 0.5f;
        [SerializeField] private float minJumpHeight = 1.5f;

        [Header("Wall Running")] 
        [SerializeField] private float wallRunGrav;
        [SerializeField] private float wallRunJumpForce;

        [Header("Camera")]
        [SerializeField] private Camera cam;
        [SerializeField] private float fov;
        [SerializeField] private float wallRunFOV;
        [SerializeField] private float wallRunFOVTime;
        [SerializeField] private float camTilt;
        [SerializeField] private float camTiltTime;

        public float tilt
        {
                get; private set;
        }
        
        private bool wallLeft;
        private bool wallRight;

        private RaycastHit leftWallHit;
        private RaycastHit rightWallHit;

        private Rigidbody Rigid;

        bool CanWallRun()
        {
                return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight);
        }

        private void Start()
        {
                Rigid = GetComponent<Rigidbody>();
        }

        private void Update()
        {
                CheckWall();
                if (CanWallRun())
                {
                        if (wallLeft && !wallRight)
                        {
                                Debug.Log("wall running on the left");
                                
                                StartWallRun();
                        }
                        else if (wallRight && !wallLeft)
                        {
                                Debug.Log("wall running on the Right");
                                
                                StartWallRun();
                        }
                        else
                        {
                                EndWallRun();
                        }
                }
                else
                {
                        EndWallRun();
                }
        }

        void CheckWall()
        {
                wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallDistance);
                wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallDistance);
        }

        void StartWallRun()
        {
                Rigid.useGravity = false;
                Rigid.AddForce(Vector3.down * wallRunGrav, ForceMode.Force);

                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView,wallRunFOV, wallRunFOVTime * Time.deltaTime);

                if (wallLeft)
                {
                        tilt = Mathf.Lerp(tilt, -camTilt, camTiltTime * Time.deltaTime);
                }
                else if (wallRight)
                {
                        tilt = Mathf.Lerp(tilt, camTilt, camTiltTime * Time.deltaTime);
                }
                
                if (Input.GetButtonDown("Jump"))
                {
                        if (wallLeft)
                        {
                                Vector3 wallRunJumpDirection = transform.up + leftWallHit.normal;
                                Rigid.velocity = new Vector3(Rigid.velocity.x, 0, Rigid.velocity.z);
                                Rigid.AddForce(wallRunJumpDirection * wallRunJumpForce * 100, ForceMode.Force);
                        }
                        else if (wallRight)
                        {
                                Vector3 wallRunJumpDirection = transform.up + rightWallHit.normal;
                                Rigid.velocity = new Vector3(Rigid.velocity.x, 0, Rigid.velocity.z);
                                Rigid.AddForce(wallRunJumpDirection * wallRunJumpForce * 100, ForceMode.Force);
                        }
                }
        }

        void EndWallRun()
        {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView,fov, wallRunFOVTime * Time.deltaTime);
                tilt = Mathf.Lerp(tilt, 0, camTiltTime * Time.deltaTime);
                
                Rigid.useGravity = true;
        }
}
