using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    FPSController controller;
    Inventory inventory;

    public float lookRange = 2f;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<Inventory>(); // Gets player's inventory
        controller = GetComponent<FPSController>(); // Gets player's FPS controller
    }

    // Update is called once per frame
    void Update()
    {
        GameObject obj = ObjectAimedAt();
        if (Interact() && obj != null) { // if player interacted, and they are aiming at something (obj)... note that this uses the lookRange
            if (obj.tag == "Holdable")
            {
                Debug.Log(inventory.AddToInventory(obj)); // Adds object to inventory (and prints result to screen)
            }
            else if (obj.tag == "Lock") {
                // Calls the Unlock function on the Lock script of the aimed at object
                // Note the "?". This makes it so it will only run if the Lock script is not null (meaning it has the script)
                // Also prints result to screen
                Debug.Log(obj.GetComponent<Lock>()?.Unlock(inventory.GetInventory()));
            }
            else if (obj.tag == "Openable") {
                // Calls the Unlock function on the Lock script of the aimed at object
                // Note the "?". This makes it so it will only run if the Lock script is not null (meaning it has the script)
                // Also prints result to screen
                obj.GetComponent<Openable>()?.OpenClose();
            }
        }
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
            return hit.collider.gameObject;
        }
        return null;
    }
}
