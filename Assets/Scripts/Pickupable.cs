using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    private Player player;

    public bool isPickedUp;

    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Pickup() {
        if (player.heldObject == null) {
            player.heldObject = gameObject;
            player.CopyHeldItemToHand();
            isPickedUp = true;
            meshRenderer.enabled = false;
            foreach (MeshRenderer childRenderer in gameObject.GetComponentsInChildren(typeof(MeshRenderer))) { // Could be laggy
                childRenderer.enabled = false;
            }
        }
        else if (player.heldObject == gameObject) {
            player.heldObject = null;
            player.ClearHand();
            isPickedUp = false;

            meshRenderer.enabled = true;
            foreach (MeshRenderer childRenderer in gameObject.GetComponentsInChildren(typeof(MeshRenderer))) { // Could be laggy
                childRenderer.enabled = true;
            }
        }
    }

    public void RemoveItem() {
        gameObject.SetActive(false);
        player.heldObject = null;
    }
}
