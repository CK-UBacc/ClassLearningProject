using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed = 1f;

    InputActionAsset InputActions;
    InputAction move;
    Vector2 moveDirection = Vector2.zero;



    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    void Awake()
    {
        move = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();

        transform.Translate(
            moveDirection.x * speed * Time.deltaTime, //Left and right Directions
            0, //Up and down directions. Set to 0 as it is never used
            moveDirection.y * speed * Time.deltaTime); // Forward and backward directions
    }
}
