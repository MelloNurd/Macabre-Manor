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

        // 6 = "Pickupable"
        // 7 = "Interactable"

        if (Interact()) { // if player interacted, and they are aiming at something (obj)... note that this uses the lookRange

            if (obj != null && obj.TryGetComponent(out Interactable component2)) {
                component2.OnInteract();
            }

            //if (obj.tag == "Holdable")
            //{
            //    Debug.Log(inventory.AddToInventory(obj)); // Adds object to inventory (and prints result to screen)
            //}
            //else if (obj.tag == "Lock") {
            //    // Calls the Unlock function on the Lock script of the aimed at object
            //    // Note the "?". This makes it so it will only run if the Lock script is not null (meaning it has the script)
            //    // Also prints result to screen
            //    Debug.Log(obj.GetComponent<Lock>()?.Unlock(inventory.GetInventory()));
            //}
            //else if (obj.tag == "Openable") {
            //    // Calls the Unlock function on the Lock script of the aimed at object
            //    // Note the "?". This makes it so it will only run if the Lock script is not null (meaning it has the script)
            //    // Also prints result to screen
            //    obj.GetComponent<Openable>()?.OpenClose();
            //}
            //else if (obj.tag == "Hint")
            //{
            //    hintText = obj.transform.GetChild(0).gameObject;
            //    Debug.Log("Hint clicked");
            //    hintText.SetActive(true);
            //    Debug.Log(hintText.name + " activated");
            //    StartCoroutine(HintWait(3.0f));
            //}
        }
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
    private bool Interact() { 
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
