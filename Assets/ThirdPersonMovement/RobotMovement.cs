using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RobotMovement : MonoBehaviour
{
    CharacterController robotControler;

    [SerializeField] float speed = 1f;
    [SerializeField] float jumpSpeed = 1f;
    [SerializeField] float gravity = 9.81f;
    [SerializeField] Transform cameraTransform;

    //private float moveX = 0;
    //private float moveY = 0;
    [Header("Internal variables")]
    public Vector2 moveInput = Vector2.zero;
    public Vector3 moveDirection = Vector3.zero;
    public float targetAngle = 0f;

    public bool isGrounded = false;
    public float verticalVelocity = 0f;

    void Awake()
    {
        robotControler = GetComponent<CharacterController>();
    }
    private void OnMove(InputValue inputValue)
    {
        //Sets the move directioni variables when input action move has changed
        //moveX = inputValue.Get<Vector2>().x;
        //moveY = inputValue.Get<Vector2>().y;

        moveInput = inputValue.Get<Vector2>();

        //Debug.Log("moveDirection [" +  moveDirection + "]");
        //Debug.Log("targetAngle [" + targetAngle + "]");
    }

    //private void OnLook(InputValue inputValue)
    //{
    //    Debug.Log("LOOKING!");
    //}

    private void OnJump(InputValue inputValue)
    {
        //rb.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);
        //if (isGrounded) robotControler.Move(Vector3.up * jumpSpeed);

        if (robotControler.isGrounded){
            verticalVelocity = jumpSpeed;
        }

    }

    private void Update()
    {
        //robotControler.Move(new Vector3(moveX, 0, moveY) * speed);

        //float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

        //Mathf.SmoothDampAngle()
        isGrounded = robotControler.isGrounded;

        targetAngle = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

        moveDirection = cameraTransform.forward * moveInput.y + cameraTransform.right * moveInput.x;

        if (!(robotControler.isGrounded))
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        //float smoothAngle = Mathf.

        transform.rotation = Quaternion.Euler(0, targetAngle, 0);
        
        robotControler.Move(moveDirection * speed * Time.deltaTime + Vector3.up * verticalVelocity);

        
        //robotControler.SimpleMove(moveDirection * speed);

        //robotControler.Move(Vector3.down * gravity);
    }

}
