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
    [SerializeField] GameObject cameraPivot;
    Vector3 camStartPos;

    public float walkSpeed = 2.5f;
    public float runSpeed = 5f;

    public float lookSpeed = 2.25f;

    public float lookXLimit = 89f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;
    public bool isMoving;
    bool isRunning;

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
        camStartPos = cameraPivot.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // Player Movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        isRunning = Input.GetKey(KeyCode.LeftShift);
        float moveSpeed = isRunning ? runSpeed : walkSpeed;
        float footStepDelay = isRunning ? 0.4f : 0.7f;

        float curSpeedX = Input.GetAxisRaw("Vertical");
        float curSpeedY = Input.GetAxisRaw("Horizontal");

        isMoving = curSpeedX != 0 || curSpeedY != 0;

        moveDirection = canMove ? (forward * curSpeedX) + (right * curSpeedY) + Vector3.down : Vector3.down;

        controller.Move(moveDirection.normalized * Time.deltaTime * moveSpeed);
        if (canMove && isMoving) {
            if(playFootstep) StartCoroutine(PlayFootstepClip(footStepDelay));
            StartHeadBob();
        }
        StopHeadBob();

        // Camera Movement
        if(canMove) {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    float Amount = 0.002f;
    float Frequency = 10.0f;
    float Smooth = 10.0f;

    private Vector3 StartHeadBob() {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * Frequency) * Amount;
        pos.x += Mathf.Cos(Time.time * Frequency / 2f) * Amount / 1.2f;
        cameraPivot.transform.localPosition += pos * (isRunning ? 1.8f : 1f);

        return pos;
    }

    private void StopHeadBob() {
        if (cameraPivot.transform.localPosition == camStartPos) return;
        cameraPivot.transform.localPosition = Vector3.Lerp(cameraPivot.transform.localPosition, camStartPos, Time.deltaTime);
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
