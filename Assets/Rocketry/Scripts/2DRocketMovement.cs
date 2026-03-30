//using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    //[SerializeField] TMP_Text fuelDisplay;

    [Header("Other")]
    [SerializeField] float CollisionVelocityDetonationThreshold = 1f;
    [SerializeField] RocketryUI ui;
    [SerializeField] AudioClip soundThrust;
    [SerializeField] AudioClip soundCollide;
    [SerializeField] AudioClip soundDetonation;

    [Header("Input Actions")]
    [SerializeField] InputActionReference inputRotate; //Take axis input for 2D rotation
    [SerializeField] InputActionReference inputThrust;
    [SerializeField] InputActionReference inputReverseThrust;

    Rigidbody rb;
    Vector3 respawnPoint;
    AudioSource soundSource;

    //Old TMP UI implementation
    //private void UpdateFuelDisplay()//Made a function so it could be called in multiple places. That didn't happen. Whatever.
    //{
    //    fuelDisplay.text = fuel.ToString("F0");
    //}

    public void AddFuel()
    {
        fuel += 30;
        if (fuel > maxFuel) fuel = maxFuel;
        ui.fuelUpdate(fuel);
    }

    private void Awake()
    {
        Debug.Log(gameObject.name + " awake triggered.");
        Debug.Log("Fuel: " + fuel);
        rb = GetComponent<Rigidbody>();
        soundSource = GetComponent<AudioSource>();
        if (rb != null) Debug.Log("Found player Rigid Body");
        respawnPoint = GetComponent<Transform>().position;
        Debug.Log("Respawn point set to [" +  respawnPoint + "]");
        //ui.fuelUpdate(fuel); // This line causes the null refrence error and causes the script component to disable itself. Changing to Start() doesn't cause the error for some reason?
    }

    // OnThrust() is an automaticly generated function from the player input component.
    // OnThrust() is triggered when the thrust key is pressed or released.
    // Never mind it only triggers when pressed
    private void OnThrust()
    {
        if (!soundSource.isPlaying)
        {
            soundSource.clip = soundThrust;
            soundSource.loop = true;
            soundSource.Play();
        }
        else
        {
            soundSource.Stop();
            soundSource.loop = false;
        }
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
            ui.fuelUpdate(fuel);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("\"" + gameObject.name + "\" has collided with \"" + collision.gameObject.name + "\" with velocity [" + rb.linearVelocity.sqrMagnitude + "]");

        if (rb.linearVelocity.sqrMagnitude > CollisionVelocityDetonationThreshold)
        {
            Debug.Log("\"" + gameObject.name + "\" has detonated!");
            StartCoroutine(delayRespawn());

            return;
        }

        if (collision.gameObject.CompareTag("LandingPad")) StartCoroutine(delayNextLevel());
    }

    IEnumerator delayRespawn()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false; //Don't wan't to destroy the ship on detonation so disableing the mesh gives the apearance of destruction
        rb.isKinematic = true; //To disable movement upon detonation
        yield return new WaitForSeconds(2);

        gameObject.GetComponent<MeshRenderer>().enabled = true;
        rb.isKinematic = false;

        transform.position = respawnPoint;
        transform.rotation = Quaternion.Euler(0, 0, 0);

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reloads the level
    }

    IEnumerator delayNextLevel()
    {
        Debug.Log("WINNAR!");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
