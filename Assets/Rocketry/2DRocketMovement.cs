using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlatRocketMovement : MonoBehaviour
{
    [Header("Move values")]
    [SerializeField] float verticalThrust = 20f;
    [SerializeField] float reverseThrust = 10f;
    [SerializeField] float angularThrust = 1f;
    
    [Header("Fuel")]
    [SerializeField] float fuel = 100f;
    [SerializeField] float maxFuel = 150f;
    [SerializeField] float fuelDecayRate = 0.1f;
    [SerializeField] TMP_Text fuelDisplay;

    [Header("Other")]
    [SerializeField] float CollisionVelocityDetonationThreshold = 1f;

    [Header("Input Actions")]
    [SerializeField] InputActionReference inputRotate; //Take axis input for 2D rotation
    [SerializeField] InputActionReference inputThrust;
    [SerializeField] InputActionReference inputReverseThrust;

    Rigidbody rb;

    private void UpdateFuelDisplay()//Made a function so it could be called in multiple places. That didn't happen. Whatever.
    {
        fuelDisplay.text = fuel.ToString("F0");
    }

    public void AddFuel()
    {
        fuel += 30;
        if (fuel > maxFuel) fuel = maxFuel;
    }

    private void Awake()
    {
        Debug.Log(gameObject.name + " awake triggered.");
        rb = GetComponent<Rigidbody>();
        //spwanPoint = transform.position;
    }

    private void FixedUpdate()
    {
        if (fuel > 0)
        {
            if (inputRotate.action.ReadValue<float>() != 0) //Grabing this value each frame probably isn't the best solution but this is a quick fuel system implementation so whatever
            {
                rb.AddRelativeTorque(0, 0, inputRotate.action.ReadValue<float>() * angularThrust, ForceMode.Force);
                fuel -= fuelDecayRate;
            }
            //Debug.Log("Input axis value: [" + inputRotate.action.ReadValue<float>() + "]");

            if (inputThrust.action.IsPressed())
            {
                rb.AddRelativeForce(Vector3.up * verticalThrust, ForceMode.Force);
                fuel -= fuelDecayRate;
            }
            if (inputReverseThrust.action.IsPressed())
            {
                rb.AddRelativeForce(Vector3.down * reverseThrust, ForceMode.Force);
                fuel -= fuelDecayRate;
            }
        }

        UpdateFuelDisplay();//Updating the fuel display every frame isn't the best idea but it's probably better than updating it multiple times per frame
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("\"" + gameObject.name + "\" has collided with \"" + collision.gameObject.name + "\" with velocity [" + rb.linearVelocity.sqrMagnitude + "]");

        if (rb.linearVelocity.sqrMagnitude > CollisionVelocityDetonationThreshold)
        {
            Debug.Log("\"" + gameObject.name + "\" has detonated!");
            gameObject.GetComponent<MeshRenderer>().enabled = false; //Don't wan't to destroy the ship on detonation so disableing the mesh gives the apearance of destruction
            rb.isKinematic = true; //To disable movement upon detonation
            return;
        }

        if (collision.gameObject.CompareTag("LandingPad")) Debug.Log("WINNAR!");
    }
}
