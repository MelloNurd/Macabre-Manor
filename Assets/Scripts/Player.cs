using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    FPSController controller;

    [SerializeField] Image crosshair;
    [SerializeField] Sprite basicCrosshair, handCrosshair, lockCrosshair;

    public float lookRange = 2f;

    public GameObject heldObject; // Gameobject in the world
    public GameObject handObj; // Gameobject that is visible in hand
    public Vector3 handPos;
    public Quaternion handRot;

    public GameObject torch;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<FPSController>(); // Gets player's FPS controller
        heldObject = null;
        handObj = transform.GetChild(3).gameObject;
        handObj.SetActive(false);
        handPos = handObj.transform.localPosition;
        handRot = handObj.transform.localRotation;

        torch = transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject obj = ObjectAimedAt();

        if(obj != null && obj.TryGetComponent(out Interactable component)) {
            switch(component.crosshair) {
                case crosshairOnHover.basic:
                    crosshair.sprite = basicCrosshair;
                    break;
                case crosshairOnHover.hand:
                    crosshair.sprite = handCrosshair;
                    break;
                case crosshairOnHover.locked:
                    crosshair.sprite = lockCrosshair;
                    break;
                default:
                    crosshair.sprite = basicCrosshair;
                    break;
            }
        }
        else crosshair.sprite = basicCrosshair;

        if (Interact()) { // if player interacted, and they are aiming at something (obj)... note that this uses the lookRange

            if (obj != null && obj.TryGetComponent(out Interactable component2)) {
                component2.Interact();
            }
        }
    }

    public void DisableTorch() {
        torch.SetActive(false);
    }

    public void EnableTorch() {
        torch.SetActive(true);
    }

    public void DisableMove()
    {
        controller.canMove = false;
    }

    public void EnableMove()
    {
        controller.canMove = true;
    }

    public void CopyHeldItemToHand(bool forceShow = false) {
        if(handObj != null) Destroy(handObj);
        handObj = Instantiate(heldObject, transform.position + (transform.forward + transform.right)/2, transform.rotation, transform);
        handObj.SetActive(true);
        //handObj.GetComponent<MeshFilter>().mesh = heldObject.GetComponent<Pickupable>().modelToHold;
        if(forceShow) ShowItemInHand();
    }

    public void ClearHand() {
        if (handObj != null) Destroy(handObj);
        handObj = null;
    }

    public void UpdateItemInHand() {
        if (handObj == null) return;

        handObj.GetComponent<MeshFilter>().mesh = heldObject.GetComponent<MeshFilter>().sharedMesh;
        Material[] mats = heldObject.GetComponent<MeshRenderer>().sharedMaterials;
        handObj.GetComponent<MeshRenderer>().materials = mats;

        ShowItemInHand();
    }

    public void ShowItemInHand() {
        if (handObj == null) return;
        handObj.SetActive(true);
        foreach(Transform child in handObj.transform) {
            child.gameObject.SetActive(true);
        }
        foreach (MeshRenderer childRenderer in handObj.GetComponentsInChildren(typeof(MeshRenderer))) { // Could be laggy
            //Debug.Log(childRenderer.gameObject.name);
            childRenderer.enabled = true;
        }
    }

    public void HideItemInHand() {
        if (handObj == null) return;
        foreach (MeshRenderer childRenderer in handObj.GetComponentsInChildren(typeof(MeshRenderer))) { // Could be laggy
            childRenderer.enabled = false;
            childRenderer.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Function which is used to test if user has pressed their interaction buttons/keys
    /// </summary>
    /// <returns>True or false, if any of the inputs in the function return true.</returns>
    public bool Interact() {
        // MouseButton 0 is left click
        return Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0); 
    }

    /// <summary>
    /// Gets what the player is looking at
    /// </summary>
    /// <returns>Gameobject that the player is looking at</returns>
    private GameObject ObjectAimedAt()
    {
        // Raycasts from the playerCams position, in the direction the camera is looking, with a max distance of lookRange.
        // The RaycastHit object is returned from the Raycast, and the GameObject of the hit is returned.
        if (Physics.Raycast(controller.playerCam.transform.position, controller.playerCam.transform.forward, out var hit, lookRange, ~LayerMask.GetMask("Player")))
        {
            Debug.DrawLine(controller.playerCam.transform.position, hit.point, UnityEngine.Color.white);
            return hit.collider.gameObject;
        }
        return null;
    }
}
