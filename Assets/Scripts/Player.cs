using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    private MeshRenderer handMR;
    private MeshFilter handMF;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<FPSController>(); // Gets player's FPS controller
        heldObject = null;
        handObj = transform.GetChild(2).gameObject;
        handMR = handObj.GetComponent<MeshRenderer>(); // don't like this but it's better for performance
        handMF = handObj.GetComponent<MeshFilter>();   //
        handObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject obj = ObjectAimedAt();

        if(obj != null && obj.TryGetComponent(out Interactable component)) {
            crosshair.sprite = component.displayNormalCrosshair ? basicCrosshair : handCrosshair;
        }
        else crosshair.sprite = basicCrosshair;

        if (Interact()) { // if player interacted, and they are aiming at something (obj)... note that this uses the lookRange

            if (obj != null && obj.TryGetComponent(out Interactable component2)) {
                component2.Interact();
            }
        }
    }

    public void DisableMove()
    {
        controller.canMove = false;
    }

    public void EnableMove()
    {
        controller.canMove = true;
    }

    public void CopyHeldItemToHand() {
        handObj.SetActive(true);
        handObj.transform.localScale = heldObject.transform.localScale;
        handMF.mesh = heldObject.GetComponent<MeshFilter>().mesh;
        handMR.materials = heldObject.GetComponent<MeshRenderer>().materials;
    }

    public void ClearHand() {
        handObj.SetActive(false);
    }

    /// <summary>
    /// Function which is used to test if user has pressed their interaction buttons/keys
    /// </summary>
    /// <returns>True or false, if any of the inputs in the function return true.</returns>
    public bool Interact() { 
        // MouseButton 0 is left click
        return (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)); 
    }

    /// <summary>
    /// Gets what the player is looking at
    /// </summary>
    /// <returns>Gameobject that the player is looking at</returns>
    private GameObject ObjectAimedAt()
    {
        // Raycasts from the playerCams position, in the direction the camera is looking, with a max distance of lookRange.
        // The RaycastHit object is returned from the Raycast, and the GameObject of the hit is returned.
        if (Physics.Raycast(controller.playerCam.transform.position, controller.playerCam.transform.forward, out var hit, lookRange))
        {
            Debug.DrawRay(controller.playerCam.transform.position, hit.point);
            return hit.collider.gameObject;
        }
        return null;
    }
}
