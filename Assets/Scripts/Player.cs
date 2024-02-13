using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    FPSController controller;
    Inventory inventory;
   

    public float lookRange = 2f;

    public GameObject lookedAtObj;
    private GameObject hintText;

    [SerializeField] Material highlightMat;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<Inventory>(); // Gets player's inventory
        controller = GetComponent<FPSController>(); // Gets player's FPS controller

        lookedAtObj = null;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject obj = ObjectAimedAt();
        if(lookedAtObj != obj) { // Outline handling

            if(obj != null && (obj.tag == "Lock" || obj.tag == "Openable" || obj.tag == "Holdable")) AddHighlightMaterial(obj);
            RemoveHighlightMaterial(lookedAtObj);
            lookedAtObj = obj;
        }

        if (Interact() && lookedAtObj != null) { // if player interacted, and they are aiming at something (obj)... note that this uses the lookRange
            if (lookedAtObj.tag == "Holdable")
            {
                Debug.Log(inventory.AddToInventory(lookedAtObj)); // Adds object to inventory (and prints result to screen)
            }
            else if (lookedAtObj.tag == "Lock") {
                // Calls the Unlock function on the Lock script of the aimed at object
                // Note the "?". This makes it so it will only run if the Lock script is not null (meaning it has the script)
                // Also prints result to screen
                Debug.Log(lookedAtObj.GetComponent<Lock>()?.Unlock(inventory.GetInventory()));
            }
            else if (lookedAtObj.tag == "Openable") {
                // Calls the Unlock function on the Lock script of the aimed at object
                // Note the "?". This makes it so it will only run if the Lock script is not null (meaning it has the script)
                // Also prints result to screen
                lookedAtObj.GetComponent<Openable>()?.OpenClose();
            }
            else if (lookedAtObj.tag == "Hint")
            {
                hintText = lookedAtObj.transform.GetChild(0).gameObject;
                Debug.Log("Hint clicked");
                hintText.SetActive(true);
                Debug.Log(hintText.name + " activated");
                StartCoroutine(HintWait(3.0f));
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
            Debug.DrawRay(controller.playerCam.transform.position, hit.point);
            return hit.collider.gameObject;
        }
        return null;
    }

    private void AddHighlightMaterial(GameObject obj) {
        MeshRenderer renderer = obj?.GetComponent<MeshRenderer>();
        if (!renderer) return;
        var materials = renderer.sharedMaterials.ToList();
        if (!materials.Contains(highlightMat)) materials.Add(highlightMat);
        renderer.materials = materials.ToArray();
    }
    private void RemoveHighlightMaterial(GameObject obj) {
        MeshRenderer renderer = obj?.GetComponent<MeshRenderer>();
        if (!renderer) return;
        var materials = renderer.sharedMaterials.ToList();
        if(materials.Contains(highlightMat)) materials.Remove(highlightMat);
        renderer.materials = materials.ToArray();
    }

    IEnumerator HintWait(float time)
    {
        yield return new WaitForSeconds(time);
        hintText.SetActive(false);
        Debug.Log(hintText.name + " deactivated");
    }
}
