using UnityEngine;

public class CameraTeleport : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject cameraTarget;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mainCamera.transform.position = cameraTarget.transform.position;
            mainCamera.transform.rotation = cameraTarget.transform.rotation;
            Debug.Log("Main camera rotation: " + mainCamera.transform.rotation.y);
        }
    }
}
