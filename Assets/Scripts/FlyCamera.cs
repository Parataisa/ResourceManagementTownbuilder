using UnityEngine;

public class FlyCamera : MonoBehaviour
    {
    public Transform target;
    float cameraSpeed = 40f;
    public float distance = 2.0f;
    public float distanceMin = 10f;
    public float distanceMax = 10f;
    float rotationYAxis = 0.0f;
    float rotationXAxis = 0.0f;
    float velocityX = 0.0f;
    float velocityY = 0.0f;
    public float yMinLimit = -90f;
    public float yMaxLimit = 90f;
    public float smoothTime = 2f;

    private Vector3 GetBaseInput()
        {
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.W))
            {
            p_Velocity += new Vector3(0, 0, cameraSpeed);
            }
        if (Input.GetKey(KeyCode.S))
            {
            p_Velocity += new Vector3(0, 0, -cameraSpeed);
            }
        if (Input.GetKey(KeyCode.A))
            {
            p_Velocity += new Vector3(-cameraSpeed, 0, 0);
            }
        if (Input.GetKey(KeyCode.D))
            {
            p_Velocity += new Vector3(cameraSpeed, 0, 0);
            }
        return p_Velocity;
        }
    private void LateUpdate()
        {
        Vector3 p = GetBaseInput();
        p *= Time.deltaTime;
        Vector3 newPosition = transform.position;
        transform.Translate(p);
        newPosition.x = transform.position.x;
        newPosition.z = transform.position.z;
        transform.position = newPosition;
        if (Input.GetMouseButton(1))
            {
            rotationYAxis += cameraSpeed * 2 *  Input.GetAxis("Mouse X") * distance * 0.02f;
            rotationXAxis -= cameraSpeed * 3 * Input.GetAxis("Mouse Y") * 0.02f;
            }
        Quaternion toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
        Quaternion rotation = toRotation;

        transform.position = newPosition;
        transform.rotation = rotation;
        velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
        velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);
        }
    }