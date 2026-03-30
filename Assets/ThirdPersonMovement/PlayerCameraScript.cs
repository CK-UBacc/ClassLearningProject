using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraScript : MonoBehaviour
{

    [SerializeField] Transform playerTransform;

    [SerializeField] float cameraMoveSpeed = 1f;
    [SerializeField] float cameraRotateSens = 1f;
    [SerializeField] Vector3 cameraOffset;

    [SerializeField] InputActionAsset inputActions;

    //private InputActionMap actionMap;
    private InputAction lookAction;

    float horzLookAngle;
    float vertLookAngle;

    private void Awake()
    {
        //actionMap = inputActions.FindActionMap("Player");
        lookAction = inputActions.FindAction("Player/Look");
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    void Update()
    {
        horzLookAngle += lookAction.ReadValue<Vector2>().x * Time.deltaTime * cameraRotateSens;

        //To prevent horzLookAngle from being a very large number. Not Essential
        if (horzLookAngle > 360) horzLookAngle -= 360;
        if (horzLookAngle < -360) horzLookAngle += 360;

        vertLookAngle -= lookAction.ReadValue<Vector2>().y * Time.deltaTime * cameraRotateSens;
        vertLookAngle = Mathf.Clamp(vertLookAngle, -90f, 90f);

        Quaternion desiredRotation = Quaternion.Euler(vertLookAngle, horzLookAngle, 0);


        transform.position = Vector3.Lerp(transform.position, playerTransform.position + cameraOffset, cameraMoveSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, cameraRotateSens * Time.deltaTime);
    }
}
