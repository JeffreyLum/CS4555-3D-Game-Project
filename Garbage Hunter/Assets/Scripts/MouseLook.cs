using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // Start is called before the first frame update

    public float mouseSensitivity = 100f;
    public Transform playerBody;
    private float xRotation = 0f;

    private Camera playerCamera;
    private float targetFOV;
    private float FOV;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerCamera=GetComponent<Camera>();
        targetFOV = playerCamera.fieldOfView;
        FOV = targetFOV;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        float fovSpeed = 4f;
        FOV = Mathf.Lerp(FOV, targetFOV, Time.deltaTime * fovSpeed);
        playerCamera.fieldOfView = FOV;
    }

    public void setCameraFOV(float targetFOV)
    {
        this.targetFOV = targetFOV;
    }
}
