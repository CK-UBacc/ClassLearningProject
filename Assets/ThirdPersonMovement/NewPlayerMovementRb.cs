using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerMovementRb : MonoBehaviour
{
    public float acceleration = 10f;
    public float jumpForce = 10f;

    private Rigidbody rb;
    private float moveX = 0;
    private float moveY = 0;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    //Requires player input component attached to player
    //OnMove and OnJump are automaticly generated functions and come with the base input system asset
    //For custom input action functions they are automaticly generated with the naming convention On(nameOfInput)
    private void OnMove(InputValue inputValue)
    {
        //Sets the move directioni variables when input action move has changed
        moveX = inputValue.Get<Vector2>().x;
        moveY = inputValue.Get<Vector2>().y;
    }

    private void OnJump(InputValue inputValue)
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3 (moveX, 0, moveY);
        rb.AddRelativeForce(moveDirection * acceleration, ForceMode.Acceleration);

        //I don't know if this or the creating a variable (see above) each FixedUpdate is more efficient but its such a small performance hit I don't care
        //rb.AddRelativeForce(moveX * acceleration, 0, moveY * acceleration, ForceMode.Acceleration);
        
    }
}
