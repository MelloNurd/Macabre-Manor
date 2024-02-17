using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class FPSController : MonoBehaviour
{

    public Camera playerCam;

    public float walkSpeed = 2.5f;
    public float runSpeed = 5f;

    public float lookSpeed = 2.25f;

    public float lookXLimit = 89f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;
    public bool isMoving;

    public AudioClip[] footsteps;

    CharacterController controller;
    AudioSource audioSource;

    bool playFootstep = true;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Player Movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float moveSpeed = isRunning ? runSpeed : walkSpeed;
        float footStepDelay = isRunning ? 0.4f : 0.7f;

        float curSpeedX = Input.GetAxisRaw("Vertical");
        float curSpeedY = Input.GetAxisRaw("Horizontal");

        isMoving = curSpeedX != 0 || curSpeedY != 0;
        if (isMoving && playFootstep) StartCoroutine(PlayFootstepClip(footStepDelay));

        moveDirection = canMove ? (forward * curSpeedX) + (right * curSpeedY) + Vector3.down : Vector3.down;

        controller.Move(moveDirection.normalized * Time.deltaTime * moveSpeed);

        // Camera Movement
        if(canMove) {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    IEnumerator PlayFootstepClip(float speed) {
        Debug.Log("test");
        playFootstep = false;
        audioSource.clip = footsteps[UnityEngine.Random.Range(0, footsteps.Length)];
        audioSource.Play();
        yield return new WaitForSeconds(speed);
        playFootstep = true;
    }
}
