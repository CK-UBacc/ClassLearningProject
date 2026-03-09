using UnityEngine;

public class Script_1 : MonoBehaviour
{
    [Tooltip("Speed multiplyer in m/s")] [SerializeField] float speed = 1;

    [Tooltip("Unused")] [SerializeField] GameObject targetPositon;

    [Tooltip("The direction that the object will move in")] [SerializeField] Vector3 moveVector = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        //transform.position += new Vector3(10, -10, 10);
        Debug.Log(name + " Activated at Position: " + transform.position + " Rotation: " + transform.rotation + " Scale: " + transform.localScale);
    }
    // Update is called once per frame
    //void Update()
    //{
    //    transform.position = Vector3.Lerp(transform.position,
    //        targetPositon.transform.position, //Target position taken from position of another game object. can replace line with new Vector3(x,y,z)
    //        speed * Time.deltaTime);
    //}

    void Update()
    {
        transform.Translate(moveVector //direction that the object will be moving in. Requires Vector3 input
            * speed * Time.deltaTime);
    }
}
//if (transform.position.x < 20)
//{
//    transform.position += new Vector3 (0.1f, 0 ,0 );
//    Debug.Log(name + " moved to Position: " + transform.position);
//}