using UnityEngine;
using UnityEngine.InputSystem;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] float verticalThrust = 20f;
    [SerializeField] float reverseThrust = 10f;
    [SerializeField] float angularThrust = 1f;
    [SerializeField] float detonationThreshold = 50f;

    Rigidbody rb;
    //Vector3 rotateDirection = Vector3.zero; //No longer used
    //Vector3 spwanPoint; //For respawn stuff

    
    public InputActionReference inputRotate; //I'm using a custom Vector3 movement input
    public InputActionReference inputThrust;
    public InputActionReference inputReverseThrust;
    
    
    private void Awake()
    {
        Debug.Log(gameObject.name + " awake triggered."); //Just to see how Awake functions
        rb = GetComponent<Rigidbody>();
        //spwanPoint = transform.position;
        //thrust.action.started += inputThrust;  //Makes this not act like a thrust an more like a jump
    }

    private void Thrust(InputAction.CallbackContext context)
    {
        Debug.Log("thrusting");
        rb.AddRelativeForce(Vector3.up * verticalThrust, ForceMode.Impulse);
    }

    //private void Update()
    //{

    //    //rotateDirection = new Vector3(inputRotate.action.ReadValue<Vector2>().x, 0, inputRotate.action.ReadValue<Vector2>().y); //Old system from when using default vector 2 movement inputs

    //    //Since inputRotate is an Input action reference not an input action we need to get the action in order to get the value from the action
    //    //rotateDirection = inputRotate.action.ReadValue<Vector3>(); //No longer used
    //}

    private void FixedUpdate()
    {
        rb.AddRelativeTorque(inputRotate.action.ReadValue<Vector3>() * angularThrust, ForceMode.Force);

        if (inputThrust.action.IsPressed()) rb.AddRelativeForce(Vector3.up * verticalThrust, ForceMode.Force);
        if (inputReverseThrust.action.IsPressed()) rb.AddRelativeForce(Vector3.down * reverseThrust, ForceMode.Force);
        //Debug.Log(gameObject.name + " velocity = " + rb.linearVelocity.sqrMagnitude);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("\"" + gameObject.name + "\" has collided with \"" + collision.gameObject.name + "\" with velocity [" + rb.linearVelocity.sqrMagnitude + "]");

        if (rb.linearVelocity.sqrMagnitude > detonationThreshold)
        {
            Debug.Log("\"" + gameObject.name + "\" has detonated!");
            gameObject.GetComponent<MeshRenderer>().enabled = false; //Don't wan't to destroy the ship on detonation so disableing the mesh gives the apearance of destruction
            rb.isKinematic = true; //To disable movement upon detonation
        }
    }
}
