using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    private Player player;

    public bool isPickedUp;

    private Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        renderer = GetComponent<Renderer>();
    }

    public void Pickup() {
        if (player.heldObject == null) {
            player.heldObject = gameObject;
            player.CopyHeldItemToHand();
            isPickedUp = true;
            renderer.enabled = false;
            foreach (Renderer renderer in gameObject.GetComponentsInChildren(typeof(Renderer))) { // Could be laggy
                renderer.enabled = false;
            }
        }
        else if (player.heldObject == gameObject) {
            player.heldObject = null;
            player.ClearHand();
            isPickedUp = false;

            renderer.enabled = true;
            foreach (Renderer renderer in gameObject.GetComponentsInChildren(typeof(Renderer))) { // Could be laggy
                renderer.enabled = true;
            }
        }
    }

    public void RemoveItem() {
        gameObject.SetActive(false);
        player.heldObject = null;
    }
}
