using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveRigidBody : MonoBehaviour
{
    //public InputAction moveInput; //old and unused
    Vector2 moveVector = Vector2.zero;

    InputAction movement;
    InputAction jump;

    [SerializeField] float accelleration = 10;
    //[SerializeField] float deccelleration = -10;
    //[SerializeField] float jumpSpeed = 100;
    //[SerializeField] float maxVelocity = 10;


    private Rigidbody rb;

    private void Awake()
    {
        movement = InputSystem.actions.FindAction("Player/Move");
        jump = InputSystem.actions.FindAction("Player/Jump");
        rb = GetComponent<Rigidbody>();
        //rb.maxLinearVelocity = maxVelocity;
    }

    private void OnEnable()
    {
        movement.Enable();
        jump.Enable();
        //moveInput.Enable(); //old and unused
    }

    private void OnDisable()
    {
        movement.Disable();
        jump.Disable();
        //moveInput.Disable(); //old and unused
    }



    private void FixedUpdate()
    {
        
        rb.AddRelativeForce(movement.ReadValue<Vector2>().x * accelleration, 0, movement.ReadValue<Vector2>().y * accelleration, ForceMode.Acceleration);
        
        //rb.AddRelativeForce(movement.ReadValue<Vector2>().x * accelleration, 0, movement.ReadValue<Vector2>().y * accelleration, ForceMode.Acceleration);

        //if (jump.triggered)
        //{
        //    rb.AddRelativeForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);
        //}
        Debug.Log(rb.linearVelocity);
    }
}