using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickupable : MonoBehaviour
{
    private Player player;

    public bool isPickedUp;

    public bool swapsItem = true;

    public bool holdBackwards = false;

    public Mesh modelToHold;

    private MeshRenderer meshRenderer;

    public UnityEvent onPickup, onPutback;

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
            player.heldObject.GetComponent<Pickupable>().PutItemBack();
            MakeItemHeld(gameObject);
        }
    }

    public void ShowItemInPlace() {
        foreach (MeshRenderer childRenderer in gameObject.GetComponentsInChildren(typeof(MeshRenderer))) { // Could be laggy
            childRenderer.enabled = true;
            if(childRenderer.gameObject != gameObject) childRenderer.gameObject.SetActive(true);
            if (childRenderer.gameObject.TryGetComponent(out SpriteRenderer sprite)) {
                childRenderer.gameObject.SetActive(true);
            }
        }
    }

    public void HideItemInPlace(GameObject obj) {
        foreach (MeshRenderer childRenderer in obj.GetComponentsInChildren(typeof(MeshRenderer))) { // Could be laggy
            childRenderer.enabled = false;
            if (childRenderer.gameObject != gameObject) childRenderer.gameObject.SetActive(false);
            if(childRenderer.gameObject.TryGetComponent(out SpriteRenderer sprite)) {
                childRenderer.gameObject.SetActive(false);
            }
        }
    }

    public void MakeItemHeld(GameObject obj) {
        player.heldObject = obj;
        player.CopyHeldItemToHand();
        if (holdBackwards) player.handObj.transform.Rotate(0, 180, 0);
        isPickedUp = true;

        onPickup?.Invoke();

        foreach (MeshRenderer childRenderer in obj.GetComponentsInChildren(typeof(MeshRenderer))) { // Could be laggy
            //Debug.Log(childRenderer.gameObject.name);
            childRenderer.enabled = true;
        }
        HideItemInPlace(obj);
    }

    public virtual void PutItemBack() {
        player.heldObject = null;
        player.ClearHand();
        isPickedUp = false;

        onPutback?.Invoke();

        player.HideItemInHand();
        ShowItemInPlace();
    }

    public void RemoveItem() {
        Destroy(gameObject);
        player.heldObject = null;
        player.ClearHand();
    }
}
