using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    FPSController controller;
    Inventory inventory;

    public float lookRange = 200f;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<Inventory>(); // Gets player's inventory
        controller = GetComponent<FPSController>(); // Gets player's FPS controller
    }

    // Update is called once per frame
    void Update()
    {
        if (Interact()) {
            if (Physics.Raycast(controller.playerCam.transform.position, controller.playerCam.transform.forward, out var hit, lookRange)) {
                var obj = hit.collider.gameObject;

                if (obj.tag == "Holdable")
                {
                    Debug.Log(inventory.AddToInventory(obj));
                }
                else if (obj.tag == "Lock") {
                    Debug.Log(obj.GetComponent<Lock>().Unlock(inventory.GetInventory()));
                }
            }
        }
    }

    private bool Interact() { 
        return (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)); 
    }
}
