using UnityEngine;
using UnityEngine.InputSystem;

public class FixedCameraPlayerMove : MonoBehaviour
{
    [Header("Movement Attributes")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotateSpeed = 0.15f;
    [SerializeField] Camera mainCamera;

    //[SerializeField] InputActionReference orient;
    //[SerializeField] InputActionReference walk;


    private Rigidbody rb;
    private Quaternion lookDirection = Quaternion.identity;
    private bool turning = false;
    private bool walking = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTurn(InputValue inputValue)
    {
        //Had problem where would not look diagonaly when keys released
        //Solution was in inputmanager set WASD input to hold and hold time to 0.1

        //Player won't rotate if direction key is currently held down
        //Apears to be an input issue
        if (inputValue.Get<Vector2>() != Vector2.zero)
        {
            lookDirection = 
                Quaternion.LookRotation(new Vector3(inputValue.Get<Vector2>().x, 0, inputValue.Get<Vector2>().y))//Must use * to add Quaternions

                //Need to figure out how to get the Quaternion of the main camera for only the Y axis. rotation.y returns radians and I need degrees
                //* Quaternion.Euler(0, 45, 0) //Modify rotation based on mainCamera Y rotation
                ;
            turning = true;
            //mainCamera.transform.rotation.y
        }
        Debug.Log("Look direction: " + lookDirection);
    }
    private void OnWalk(InputValue inputValue)
    {
        walking = !walking;
    }

    private void RotateLerp()
    {
        if (turning)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, rotateSpeed);

            //To set rotation to desired direction when lerp is nearing the desired value
            if (Quaternion.Angle(transform.rotation, lookDirection) < 1)
            {
                transform.rotation = lookDirection; 
                turning = false;
            }
        }
    }

    private void FixedUpdate()
    {
        RotateLerp();

        if (walking) rb.AddRelativeForce(Vector3.forward * moveSpeed, ForceMode.Acceleration);


    }
}
