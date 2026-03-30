using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveScript : MonoBehaviour
{
    [SerializeField]
    float accelleration = 1;

    private Rigidbody rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    //private void Update()
    //{
    //    rb.linearVelocity = Vector3.forward * accelleration;
    //}

    private void FixedUpdate()
    {
        rb.AddForce( Vector3.forward * accelleration, ForceMode.Force);
    }
}
