using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 100f;
    public Vector3 offset;
    public float minAngle = -35f;
    public float maxAngle = 35f;

    private float mouseX;
    private float mouseY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        mouseX += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        mouseY = Mathf.Clamp(mouseY, minAngle, maxAngle);

        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0f);

        transform.position = player.position + rotation * offset;
        transform.LookAt(player.position);
    }
}
