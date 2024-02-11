using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{

    public Camera playerCam;

    public float walkSpeed = 6f;
    public float runSpeed = 10f;

    public float lookSpeed = 2f;

    public float lookXLimit = 89f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;

        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        controller.Move(moveDirection * Time.deltaTime);

        if(canMove) {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}
