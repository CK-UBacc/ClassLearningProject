using UnityEngine;
using UnityEngine.InputSystem;

public class FlatRocketMovement : MonoBehaviour
{
    [SerializeField] float verticalThrust = 20f;
    [SerializeField] float reverseThrust = 10f;
    [SerializeField] float angularThrust = 1f;
    [SerializeField] float detonationThreshold = 50f;

    Rigidbody rb;

    public InputActionReference inputRotate; //Take axis input for 2D rotation
    public InputActionReference inputThrust;
    public InputActionReference inputReverseThrust;

    private void Awake()
    {
        Debug.Log(gameObject.name + " awake triggered.");
        rb = GetComponent<Rigidbody>();
        //spwanPoint = transform.position;
    }

    private void FixedUpdate()
    {
        rb.AddRelativeTorque(0,0,inputRotate.action.ReadValue<float>() * angularThrust, ForceMode.Force);
        //Debug.Log("Input axis value: [" + inputRotate.action.ReadValue<float>() + "]");

        if (inputThrust.action.IsPressed()) rb.AddRelativeForce(Vector3.up * verticalThrust, ForceMode.Force);
        if (inputReverseThrust.action.IsPressed()) rb.AddRelativeForce(Vector3.down * reverseThrust, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("\"" + gameObject.name + "\" has collided with \"" + collision.gameObject.name + "\" with velocity [" + rb.linearVelocity.sqrMagnitude + "]");

        if (rb.linearVelocity.sqrMagnitude > detonationThreshold)
        {
            Debug.Log("\"" + gameObject.name + "\" has detonated!");
            gameObject.GetComponent<MeshRenderer>().enabled = false; //Don't wan't to destroy the ship on detonation so disableing the mesh gives the apearance of destruction
            rb.isKinematic = true; //To disable movement upon detonation
            return;
        }

        if (collision.gameObject.CompareTag("LandingPad")) Debug.Log("WINNAR!");
    }
}
