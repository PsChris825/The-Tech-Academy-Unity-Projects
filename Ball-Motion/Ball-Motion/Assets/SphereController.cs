using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.SceneManagement;

public class SphereController : MonoBehaviour
{
    public float speed = 5f;
    public Camera mainCamera;
    public float rotationSpeed = 5f;
    public float drag = 10f;
    public float angularDrag = 10f;

    private Rigidbody rb;
    private Quaternion targetRotation = Quaternion.identity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = drag;
        rb.angularDrag = angularDrag;
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = mainCamera.transform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = mainCamera.transform.right;
        right.y = 0f;
        right.Normalize();

        Vector3 movement = (forward * vertical + right * horizontal).normalized;

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        float distanceToGround = transform.position.y;
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0f, distanceToGround, 0f));
        if (groundPlane.Raycast(cameraRay, out float rayDistance))
        {
            Vector3 point = cameraRay.GetPoint(rayDistance);
            Vector3 lookDirection = point - transform.position;
            targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        rb.AddForce(movement * speed, ForceMode.VelocityChange);
    }
}
