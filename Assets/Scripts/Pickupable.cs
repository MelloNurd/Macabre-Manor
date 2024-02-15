using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    private Player player;

    public bool isPickedUp;

    public bool swapsItem;

    public Mesh modelToHold;

    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        meshRenderer = GetComponent<MeshRenderer>();

        if(gameObject.isStatic) gameObject.isStatic = false;
    }

    public void Pickup() {
        if (player.heldObject == null) {
            MakeItemHeld(gameObject);
        }
        else if (player.heldObject == gameObject) {
            PutItemBack();
        }
        else if(swapsItem) {
            PutItemBack();
            MakeItemHeld(gameObject);
        }
    }

    public void MakeItemHeld(GameObject obj) {
        player.heldObject = obj;
        player.CopyHeldItemToHand();
        isPickedUp = true;

        meshRenderer.enabled = false;
        foreach (MeshRenderer childRenderer in obj.GetComponentsInChildren(typeof(MeshRenderer))) { // Could be laggy
            childRenderer.enabled = false;
        }
    }

    public void PutItemBack() {
        player.heldObject = null;
        player.ClearHand();
        isPickedUp = false;

        meshRenderer.enabled = true;
        foreach (MeshRenderer childRenderer in gameObject.GetComponentsInChildren(typeof(MeshRenderer))) { // Could be laggy
            childRenderer.enabled = true;
        }
    }

    public void RemoveItem() {
        Destroy(gameObject);
        player.heldObject = null;
        player.ClearHand();
    }
}
